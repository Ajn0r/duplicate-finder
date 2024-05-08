using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DuplicateFinder
{
    public partial class MainWindow : Window
    {
        // A list of tuples to store the file information from each file
        private List<(string Name, long Size, DateTime DateCreated, DateTime DateModified, string Path)> fileInformation = new List<(string, long, DateTime, DateTime, string)>();
        // A record to store the attributes to compare the files with
        private RecordAttributes attributes; 

        public MainWindow()
        {
            InitializeComponent();
            SetFileTypesSelection();
        }

        /// <summary>
        /// Method to set the file types selection listbox with the file types te user can select
        /// </summary>
        private void SetFileTypesSelection()
        {
            FileTypeList.Items.Add(".jpg");
            FileTypeList.Items.Add(".txt");
            FileTypeList.Items.Add(".png");
            FileTypeList.Items.Add(".pdf");
            FileTypeList.Items.Add(".*");
        }

        /// <summary>
        /// Method to handle the click event of the Add Folder button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddFolder_Clicked(object sender, RoutedEventArgs e)
        {
            // Create a new instance of the CommonOpenFileDialog
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true; // Set the dialog to be a folder picker
            dialog.Multiselect = true; // Allow multiple folders to be selected
            // Show the dialog and check if the user clicked OK
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                // if the user has not selected a folder, show an error message to prevent the foreach loop to looping through null values
                if (dialog.FileName == null)
                    MessageBox.Show("Please select a folder", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                // Loop through the folders selected and add it to the listview
                foreach (var folder in dialog.FileNames)
                {
                    // check that the folder is not already in the listview
                    if (!FolderList.Items.Contains(folder))
                        // Add the folder to the listview
                        FolderList.Items.Add(folder);
                }
            }
        }

        /// <summary>
        /// Method to handle the click event of the Remove Folder button, only removes the selected folder from the listview, not the folder itself
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveFolder_Clicked(object sender, RoutedEventArgs e)
        {
            // Check if an item is selected in the listview
            if (FolderList.SelectedItem != null)
                // Remove the selected item from the listview
                FolderList.Items.Remove(FolderList.SelectedItem);
            else
                MessageBox.Show("Please select a folder to remove", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        /// <summary>
        /// Method to handle the click event of the Delete File button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteFile_Clicked(object sender, RoutedEventArgs e)
        {
            // Check if an item is selected in the listview
            if (lvResults.SelectedItem != null)
            {
                // Ask the user if they are sure they want to delete the file
                MessageBoxResult result = MessageBox.Show("Are you sure you want to delete the file?", "Delete File", MessageBoxButton.YesNo);
                // if the user clicks yes, go ahead and delete the file
                if (result == MessageBoxResult.Yes)
                {
                    // Get the path of the file to delete
                    string path = fileInformation[lvResults.SelectedIndex].Path;
                    // Delete the file
                    File.Delete(path);
                    // Remove the file from the listview
                    lvResults.Items.Remove(lvResults.SelectedItem);
                }
            } else
            {
                MessageBox.Show("Please select a file to delete", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Method to handle the click event of the Find Duplicates button 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FindDuplicates_Clicked(object sender, RoutedEventArgs e)
        {
            // Check that at least one folder has been added to the list of folders
            if (FolderList.Items.Count == 0)
            {
                MessageBox.Show("Please select a folder to search for duplicates", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // check if none of the checkboxes for attributes to compare with is checked
            if (chkName.IsChecked == false && chkSize.IsChecked == false && chkDateCreated.IsChecked == false && chkDateModified.IsChecked == false)
            {
                MessageBox.Show("Please select at least one attribute to compare the files with", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            // Check that at least one file type has been selected from the list of file types
            if (FileTypeList.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a file type to search for duplicates", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Clear the list of tuples that store the file information for each search result
            fileInformation.Clear();

            // Loop through the folders added to the list of folders 
            foreach (string folderPath in FolderList.Items)
            {
                // Using Directory.GetFiles to get all files in the folder and its subfolders and store them in an string array to be able to loop through them and compare.
                string[] files = Directory.GetFiles(folderPath, "*", SearchOption.AllDirectories);

                // Check the files and add the file information to the list of tuples
                CheckFiles(files);
            }

            // Get the selected attributes and store them in the record.
            // a check is done to see if the checkbox is checked or not, if IsChecked == true, it will send true, otherwise false to the record
            attributes = new RecordAttributes(chkName.IsChecked == true, chkDateCreated.IsChecked == true, chkDateModified.IsChecked == true, chkSize.IsChecked == true);

            // Creates a list of tuples to store all duplicate files found
            List<(string Name, long Size, DateTime DateCreated, DateTime DateModified, string Path)> duplicates = new List<(string, long, DateTime, DateTime, string)>();

            // Find all the duplicates and add them to the list of duplicates
            FindDuplicates(duplicates);

            // Fill the listview with the duplicates
            FillResultView(duplicates);
        }

        /// <summary>
        /// Method to fill the listview with the duplicates found
        /// </summary>
        /// <param name="duplicates"></param>
        private void FillResultView(List<(string Name, long Size, DateTime DateCreated, DateTime DateModified, string Path)> duplicates)
        {
            // clear the listview before adding the duplicates to avoid duplicates in the listview
            lvResults.Items.Clear();

            // Loop through the duplicates list and add the items to the listview
            foreach (var duplicate in duplicates)
            {
                // add the duplicate to the listview, could not use Binding because of the tuple so this is the alternative I came up with that works
                lvResults.Items.Add("Name: " + duplicate.Item1 + ", Size: " + duplicate.Item2 + ", Date created: " + duplicate.Item3 + ", Path: " + duplicate.Item5);
            }
            if (lvResults.Items.Count == 0)
                MessageBox.Show("No duplicates found", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Method to find duplicates in the list of file information
        /// </summary>
        /// <param name="duplicates"></param>
        private void FindDuplicates(List<(string Name, long Size, DateTime DateCreated, DateTime DateModified, string Path)> duplicates)
        {
            // Loop through the file information list and compare each file with the other files in the list
            // i starts at 0 and j starts at i + 1 to avoid comparing the same file with itself
            for (int i = 0; i < fileInformation.Count; i++)
            {
                // Nested loop to compare the file with the other files in the list
                for (int j = i + 1; j < fileInformation.Count; j++)
                {
                    // Check if the files are duplicates and add them to the list of duplicates
                    if (IsDuplicate(fileInformation[i], fileInformation[j]))
                    {
                        // add both files if they are duplicates
                        duplicates.Add(fileInformation[i]);
                        duplicates.Add(fileInformation[j]);
                    }
                }
            }
        }

        /// <summary>
        /// Method to check the files and add the file information to the list of tuples if the file matches the selected file type
        /// </summary>
        /// <param name="files"></param>
        private void CheckFiles(string[] files)
        {
            // a foreach loop to go trough all the files in the string array that was created with Directory.GetFiles to hold all files from each folder
            foreach (string file in files)
            {
                // Nested forach loop to check if the file matches the selected file type to be compared with selected by the user. 
                foreach (string selectedFileType in FileTypeList.SelectedItems)
                {
                    // Check if the file matches the selected file type or if the user has selected all file types
                    if (selectedFileType == ".*" ||
                        file.Contains(selectedFileType))
                    {
                        // Create a new instance of the FileInfo class to get the file information
                        FileInfo fileInfo = new FileInfo(file);
                        // Get the date the file was created and the date the file was last modified
                        DateTime dateCreated = File.GetCreationTime(file);
                        DateTime dateModified = File.GetLastWriteTime(file);
                        // Add the file information to the list of tuples
                        fileInformation.Add((fileInfo.Name, fileInfo.Length, dateCreated, dateModified, file));
                        // break the inner loop to avoid adding the same file multiple times
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Method to check if two files are duplicates, takes two tuples as arguments and returns a boolean for true or false.
        /// Basically takes each attribute from the two files and compares them to see if they are the same or not, if they are the same it returns true, otherwise false.
        /// using the || will return true as soon as one of the conditions are met, so if the name is the same it will return true and not check the other attributes.
        /// If all are false it will return false, meaning it is not a duplicate.
        /// </summary>
        /// <param name="file1"></param>
        /// <param name="file2"></param>
        /// <returns></returns>
        private bool IsDuplicate((string Name, long Size, DateTime DateCreated, DateTime DateModified, string Path) file1, (string Name, long Size, DateTime DateCreated, DateTime DateModified, string Path) file2)
        {
            return attributes.CompareName && file1.Name == file2.Name ||
                   attributes.CompareDateCreated && file1.DateCreated == file2.DateCreated ||
                   attributes.CompareDateModified && file1.DateModified == file2.DateModified ||
                   attributes.CompareSize && file1.Size == file2.Size;
        }
    }

    // Record for holding the attributes to compare the files with
    public record RecordAttributes(bool CompareName, bool CompareDateCreated, bool CompareDateModified, bool CompareSize);
}