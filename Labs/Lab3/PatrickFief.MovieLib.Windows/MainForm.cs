/*
 * ITSE 1430
 * Patrick Fief
 * Lab 2
 */
using System;
using System.Windows.Forms;

namespace PatrickFief.MovieLib.Windows
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void fileToolStripMenuItem_Click( object sender, EventArgs e )
        {

        }

        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnHelpAbout( object sender, EventArgs e )
        {
            var form = new MovieAboutForm();

            form.ShowDialog(this);
        }

        private void OnMovieAdd( object sender, EventArgs e )
        {
            var form = new MovieDetailForm("Add Movie");

            //Modal form
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            _movie = form.Movie;
        }

        private void OnMovieEdit( object sender, EventArgs e )
        {
            if (_movie == null)
            {
                MessageBox.Show(this, "There is no movie to edit.", "Edit Movie", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var form = new MovieDetailForm(_movie);

            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //"Editing" the Movie
            _movie = form.Movie;
        }

        private void OnMovieDelete( object sender, EventArgs e )
        {
            if(_movie == null)
            {
                MessageBox.Show(this, "There is no movie to Remove.", "Remove Movie", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (!ShowConfirmation("Are you sure?", "Remove Movie"))
                return;

            _movie = null;
        }

        private bool ShowConfirmation( string message, string title )
        {
            return MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                == DialogResult.Yes;
        }

        private Movie _movie;
    }
}
