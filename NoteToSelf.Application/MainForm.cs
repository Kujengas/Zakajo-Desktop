namespace NoteToSelf.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;

    using NoteToSelf.Application.Extensions;
    using NoteToSelf.Application.ViewModels;
    using NoteToSelf.DataAccess;
    using NoteToSelf.Framework.Configuration;
    using NoteToSelf.Framework.Logging;
    using NoteToSelf.Model;

    class MainForm : Form
    {

        private MenuStrip menuStrip1;
        private Panel noteListPanel;
        private GroupBox groupBox1;
        private DataGridView notesGrid;
        private Panel noteDetailPanel;
        private GroupBox groupBox2;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox titleTextBox;
        private ComboBox categoryDropDown;
        private RichTextBox noteTextBox;
        private Button saveButton;
        private Button cancelButton;
        private Button addButton;
        private List<NoteType> categotries;

        private Enums.DetailMode detailMode;
        private Label noteIdLabel;
        private Label label4;
        private DataGridViewTextBoxColumn noteIdColumn;
        private DataGridViewTextBoxColumn saveDateColumn;
        private DataGridViewTextBoxColumn TitleColumn;
        private DataGridViewTextBoxColumn CategoryColumn;
        private DataGridViewTextBoxColumn PreviewColumn;
        private DataGridViewButtonColumn viewButtonColumn;
        private DataGridViewButtonColumn editButtonColumn;
        private DataGridViewButtonColumn deleteButtonColumn;

        private INotesProvider noteProvider;
        private NoteSet noteSet;
        private INoteToSelfLogger logger;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem setFileLocationToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private Label lastSavedDateLabel;
        private INoteToSelfConfiguration config;

        public MainForm()
        {
            logger = new NoteToSelfEventLogger();
            this.Text = Constants.ApplicationName;
            this.InitializeComponent();
            config = new NoteToSelfConfiguration();
            if (config.ReadConfigSetting(Constants.ConfigFileLocation) == String.Empty)
            {
                this.OpenSaveFileDialog();
            }
            else
            {
                this.noteProvider = new XMLNotesProvider();
                this.noteSet = new NoteSet();
                this.noteSet = this.noteProvider.LoadNoteSet();
                this.BuildCategoryList();
                this.BindCategoryComboBox();
                this.BindNotesList();
            }
        }

        private void OpenSaveFileDialog()
        {
            SaveFileDialog setFileLocationDialog = new SaveFileDialog();

            setFileLocationDialog.Filter = Constants.SaveFileDialogFilter;
            setFileLocationDialog.FilterIndex = 1;
            setFileLocationDialog.RestoreDirectory = true;
            setFileLocationDialog.OverwritePrompt = false;
            setFileLocationDialog.DefaultExt = Constants.SaveFileDialogDefaultExt;
            setFileLocationDialog.Title = "Please select the location of the Note To Self data File.";
            if (setFileLocationDialog.ShowDialog() == DialogResult.OK)
            {
                config.UpdateConfigSetting(Constants.ConfigFileLocation, setFileLocationDialog.FileName);
                this.noteProvider = new XMLNotesProvider();
                this.noteSet = new NoteSet();
                this.noteSet = this.noteProvider.LoadNoteSet();

                this.BuildCategoryList();
                this.BindCategoryComboBox();
                this.BindNotesList();
            }
        }

        private void BindNotesList()
        {
            try
            {
                this.noteIdColumn.DataPropertyName = "Id";
                this.saveDateColumn.DataPropertyName = "CreationDate";
                this.TitleColumn.DataPropertyName = "Name";
                this.CategoryColumn.DataPropertyName = "CategoryName";
                this.PreviewColumn.DataPropertyName = "Text";
                var source = new BindingSource(noteSet.Notes.ToViewModel(), null);
                this.notesGrid.AutoGenerateColumns = false;
                this.notesGrid.DataSource = source;
                this.lastSavedDateLabel.Text = String.Format("Last saved: {0:g}", noteSet.LastSaveDate);
            }
            catch (Exception ex)
            {
                logger.WriteMessage("MainForm.BindNotesList()", ex.Message);
            }
        }

        private void BindCategoryComboBox()
        {
            try
            {
                this.categoryDropDown.ValueMember = "Id";
                this.categoryDropDown.DisplayMember = "Name";
                this.categoryDropDown.DataSource = new BindingSource(this.categotries, null);
                this.categoryDropDown.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                logger.WriteMessage("MainForm.BindCategoryComboBox():", ex.Message);
            }
        }

        private void BuildCategoryList()
        {
            //TODO:-RR Move to model. For the long term, convert this to be user configurable as time allows
            categotries = new List<NoteType>();
            categotries.Add(new NoteType { Id = 1, Name = "To Do List" });
            categotries.Add(new NoteType { Id = 2, Name = "Web Links" });
            categotries.Add(new NoteType { Id = 3, Name = "Code Snippets" });
            categotries.Add(new NoteType { Id = 4, Name = "New Project Ideas" });
            categotries.Add(new NoteType { Id = 5, Name = "General Notes" });
            categotries.Add(new NoteType { Id = 6, Name = "Misc" });

        }

        private void HidePanels()
        {
            this.noteDetailPanel.Hide();
            this.noteListPanel.Hide();
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setFileLocationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noteListPanel = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lastSavedDateLabel = new System.Windows.Forms.Label();
            this.notesGrid = new System.Windows.Forms.DataGridView();
            this.noteIdColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.saveDateColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TitleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CategoryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PreviewColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.viewButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.editButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.deleteButtonColumn = new System.Windows.Forms.DataGridViewButtonColumn();
            this.addButton = new System.Windows.Forms.Button();
            this.noteDetailPanel = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.noteIdLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.titleTextBox = new System.Windows.Forms.TextBox();
            this.categoryDropDown = new System.Windows.Forms.ComboBox();
            this.noteTextBox = new System.Windows.Forms.RichTextBox();
            this.saveButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.noteListPanel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.notesGrid)).BeginInit();
            this.noteDetailPanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(960, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setFileLocationToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // setFileLocationToolStripMenuItem
            // 
            this.setFileLocationToolStripMenuItem.Name = "setFileLocationToolStripMenuItem";
            this.setFileLocationToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.setFileLocationToolStripMenuItem.Text = "Set File Location";
            this.setFileLocationToolStripMenuItem.Click += new System.EventHandler(this.setFileLocationToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // noteListPanel
            // 
            this.noteListPanel.Controls.Add(this.groupBox1);
            this.noteListPanel.Location = new System.Drawing.Point(16, 32);
            this.noteListPanel.Name = "noteListPanel";
            this.noteListPanel.Size = new System.Drawing.Size(936, 476);
            this.noteListPanel.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lastSavedDateLabel);
            this.groupBox1.Controls.Add(this.notesGrid);
            this.groupBox1.Controls.Add(this.addButton);
            this.groupBox1.Location = new System.Drawing.Point(3, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(930, 470);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Notes";
            // 
            // lastSavedDateLabel
            // 
            this.lastSavedDateLabel.AutoSize = true;
            this.lastSavedDateLabel.Location = new System.Drawing.Point(6, 444);
            this.lastSavedDateLabel.Name = "lastSavedDateLabel";
            this.lastSavedDateLabel.Size = new System.Drawing.Size(0, 13);
            this.lastSavedDateLabel.TabIndex = 3;
            // 
            // notesGrid
            // 
            this.notesGrid.AllowUserToAddRows = false;
            this.notesGrid.AllowUserToDeleteRows = false;
            this.notesGrid.AllowUserToResizeColumns = false;
            this.notesGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.notesGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.noteIdColumn,
            this.saveDateColumn,
            this.TitleColumn,
            this.CategoryColumn,
            this.PreviewColumn,
            this.viewButtonColumn,
            this.editButtonColumn,
            this.deleteButtonColumn});
            this.notesGrid.Location = new System.Drawing.Point(6, 21);
            this.notesGrid.Name = "notesGrid";
            this.notesGrid.ReadOnly = true;
            this.notesGrid.RowTemplate.Height = 24;
            this.notesGrid.Size = new System.Drawing.Size(918, 407);
            this.notesGrid.TabIndex = 1;
            this.notesGrid.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.notesGrid_CellClick);
            // 
            // noteIdColumn
            // 
            this.noteIdColumn.HeaderText = "Note Id";
            this.noteIdColumn.Name = "noteIdColumn";
            this.noteIdColumn.ReadOnly = true;
            // 
            // saveDateColumn
            // 
            this.saveDateColumn.HeaderText = "Save Date";
            this.saveDateColumn.Name = "saveDateColumn";
            this.saveDateColumn.ReadOnly = true;
            // 
            // TitleColumn
            // 
            this.TitleColumn.HeaderText = "Title";
            this.TitleColumn.Name = "TitleColumn";
            this.TitleColumn.ReadOnly = true;
            // 
            // CategoryColumn
            // 
            this.CategoryColumn.HeaderText = "Category";
            this.CategoryColumn.Name = "CategoryColumn";
            this.CategoryColumn.ReadOnly = true;
            // 
            // PreviewColumn
            // 
            this.PreviewColumn.HeaderText = "Preview";
            this.PreviewColumn.Name = "PreviewColumn";
            this.PreviewColumn.ReadOnly = true;
            // 
            // viewButtonColumn
            // 
            this.viewButtonColumn.HeaderText = "View";
            this.viewButtonColumn.Name = "viewButtonColumn";
            this.viewButtonColumn.ReadOnly = true;
            this.viewButtonColumn.Text = "View";
            this.viewButtonColumn.UseColumnTextForButtonValue = true;
            // 
            // editButtonColumn
            // 
            this.editButtonColumn.HeaderText = "Edit";
            this.editButtonColumn.Name = "editButtonColumn";
            this.editButtonColumn.ReadOnly = true;
            this.editButtonColumn.Text = "Edit";
            this.editButtonColumn.UseColumnTextForButtonValue = true;
            // 
            // deleteButtonColumn
            // 
            this.deleteButtonColumn.HeaderText = "Delete";
            this.deleteButtonColumn.Name = "deleteButtonColumn";
            this.deleteButtonColumn.ReadOnly = true;
            this.deleteButtonColumn.Text = "Delete";
            this.deleteButtonColumn.UseColumnTextForButtonValue = true;
            // 
            // addButton
            // 
            this.addButton.Location = new System.Drawing.Point(786, 434);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(138, 30);
            this.addButton.TabIndex = 0;
            this.addButton.Text = "Add New Note";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // noteDetailPanel
            // 
            this.noteDetailPanel.Controls.Add(this.groupBox2);
            this.noteDetailPanel.Location = new System.Drawing.Point(15, 31);
            this.noteDetailPanel.Name = "noteDetailPanel";
            this.noteDetailPanel.Size = new System.Drawing.Size(936, 476);
            this.noteDetailPanel.TabIndex = 2;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.noteIdLabel);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.titleTextBox);
            this.groupBox2.Controls.Add(this.categoryDropDown);
            this.groupBox2.Controls.Add(this.noteTextBox);
            this.groupBox2.Controls.Add(this.saveButton);
            this.groupBox2.Controls.Add(this.cancelButton);
            this.groupBox2.Location = new System.Drawing.Point(4, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(917, 463);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Note Details";
            // 
            // noteIdLabel
            // 
            this.noteIdLabel.AutoSize = true;
            this.noteIdLabel.Location = new System.Drawing.Point(60, 25);
            this.noteIdLabel.Name = "noteIdLabel";
            this.noteIdLabel.Size = new System.Drawing.Size(0, 13);
            this.noteIdLabel.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(7, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(45, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Note Id:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Note Text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Category";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Title";
            // 
            // titleTextBox
            // 
            this.titleTextBox.Location = new System.Drawing.Point(6, 76);
            this.titleTextBox.Name = "titleTextBox";
            this.titleTextBox.Size = new System.Drawing.Size(318, 20);
            this.titleTextBox.TabIndex = 4;
            // 
            // categoryDropDown
            // 
            this.categoryDropDown.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.categoryDropDown.FormattingEnabled = true;
            this.categoryDropDown.ItemHeight = 13;
            this.categoryDropDown.Location = new System.Drawing.Point(7, 128);
            this.categoryDropDown.Name = "categoryDropDown";
            this.categoryDropDown.Size = new System.Drawing.Size(318, 21);
            this.categoryDropDown.TabIndex = 5;
            this.categoryDropDown.KeyDown += new System.Windows.Forms.KeyEventHandler(this.categoryDropDown_KeyDown);
            // 
            // noteTextBox
            // 
            this.noteTextBox.Location = new System.Drawing.Point(7, 177);
            this.noteTextBox.Name = "noteTextBox";
            this.noteTextBox.Size = new System.Drawing.Size(904, 238);
            this.noteTextBox.TabIndex = 6;
            this.noteTextBox.Text = "";
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(695, 421);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(105, 35);
            this.saveButton.TabIndex = 7;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // cancelButton
            // 
            this.cancelButton.Location = new System.Drawing.Point(806, 421);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(105, 35);
            this.cancelButton.TabIndex = 8;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(960, 515);
            this.Controls.Add(this.noteListPanel);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.noteDetailPanel);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.noteListPanel.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.notesGrid)).EndInit();
            this.noteDetailPanel.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void addButton_Click(object sender, EventArgs e)
        {
            detailMode = Enums.DetailMode.Add;
            this.HidePanels();
            noteDetailPanel.Show();
            titleTextBox.Focus();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            this.HidePanels();
            noteListPanel.Show();
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.ClearNoteDetailPanel();
            this.HidePanels();
            this.detailMode = Enums.DetailMode.None;
            this.noteListPanel.Show();
            this.SetDetailPanelReadOnlyStatus(false);
        }

        private void ClearNoteDetailPanel()
        {
            this.noteTextBox.Text = "";
            this.categoryDropDown.SelectedIndex = -1;
            this.titleTextBox.Text = "";
            this.noteIdLabel.Text = "";
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            try
            {
                switch (detailMode)
                {

                    case Enums.DetailMode.Add:
                        noteSet.Notes.Add(
                            new Note
                                {
                                    CreationDate = DateTime.Now,
                                    Id = noteSet.MaxIdentity,
                                    Name = titleTextBox.Text,
                                    Text = noteTextBox.Text,
                                    Type =
                                        new NoteType { Id = (int)categoryDropDown.SelectedValue, Name = categoryDropDown.Text }
                                });


                        break;
                    case Enums.DetailMode.Edit:
                        int noteId;
                        int.TryParse(noteIdLabel.Text, out noteId);
                        foreach (Note n in noteSet.Notes)
                        {
                            if (n.Id == noteId)
                            {
                                n.CreationDate = DateTime.Now;
                                n.Name = this.titleTextBox.Text;
                                n.Text = this.noteTextBox.Text;
                                n.Type.Id = (int)this.categoryDropDown.SelectedValue;
                                n.Type.Name = this.categoryDropDown.Text;
                            }
                        }
                        break;
                }
                this.HidePanels();

                this.ClearNoteDetailPanel();
                this.noteListPanel.Show();
                noteProvider.SaveNoteSet(noteSet);
                this.BindNotesList();
            }
            catch (Exception ex)
            {
                logger.WriteMessage("MainForm.saveButton_click():", ex.Message);
            }
        }

        private void notesGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex < 0 || e.ColumnIndex < this.notesGrid.Columns["viewButtonColumn"].Index)
                {
                    return;
                }
                if (e.ColumnIndex == this.notesGrid.Columns["viewButtonColumn"].Index)
                {
                    var data = (NoteViewModel)this.notesGrid.Rows[e.RowIndex].DataBoundItem;

                    this.PopulateDetailView(data);
                    this.SetDetailPanelReadOnlyStatus(true);
                    this.detailMode = Enums.DetailMode.View;
                    this.HidePanels();
                    this.noteDetailPanel.Show();
                    cancelButton.Focus();

                }
                else if (e.ColumnIndex == this.notesGrid.Columns["editButtonColumn"].Index)
                {
                    var data = (NoteViewModel)this.notesGrid.Rows[e.RowIndex].DataBoundItem;
                    this.PopulateDetailView(data);
                    this.SetDetailPanelReadOnlyStatus(false);
                    this.detailMode = Enums.DetailMode.Edit;
                    this.HidePanels();
                    this.noteDetailPanel.Show();
                    titleTextBox.Focus();

                }
                else if (e.ColumnIndex == this.notesGrid.Columns["deleteButtonColumn"].Index)
                {
                    var data = (NoteViewModel)this.notesGrid.Rows[e.RowIndex].DataBoundItem;
                    var noteToRemove = this.noteSet.Notes.SingleOrDefault(n => n.Id == data.Id);
                    if (noteToRemove != null) this.noteSet.Notes.Remove(noteToRemove);
                    noteProvider.SaveNoteSet(noteSet);
                    this.BindNotesList();
                }
            }
            catch (Exception ex)
            {
                logger.WriteMessage("MainForm.notesGrid_CellClick():", ex.Message);
            }

        }

        private void PopulateDetailView(NoteViewModel data)
        {
            this.titleTextBox.Text = data.Name;
            this.categoryDropDown.SelectedValue = data.CategoryId;
            this.noteTextBox.Text = data.Text;
            this.noteIdLabel.Text = data.Id.ToString();

        }

        private void SetDetailPanelReadOnlyStatus(bool isReadOnly)
        {
            if (isReadOnly)
            {
                this.titleTextBox.ReadOnly = true;
                this.categoryDropDown.Enabled = false;
                this.noteTextBox.ReadOnly = true;
                this.saveButton.Hide();
            }
            else
            {
                this.titleTextBox.ReadOnly = false;
                this.categoryDropDown.Enabled = true;
                this.noteTextBox.ReadOnly = false;
                this.saveButton.Show();
            }
        }

        private void setFileLocationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.OpenSaveFileDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        private void categoryDropDown_KeyDown(object sender, KeyEventArgs e)
        {
            e.SuppressKeyPress = true;
        }
    }
}
