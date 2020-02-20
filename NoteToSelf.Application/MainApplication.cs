namespace NoteToSelf.Application
{
    using System;

    using Application = System.Windows.Forms.Application;

    class MainApplication
    {
        [STAThread]
        public static void Main()
        {
           var form = new MainForm();
            Application.Run(form);

        }
    }
}
