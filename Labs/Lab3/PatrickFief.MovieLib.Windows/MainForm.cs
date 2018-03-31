/*
 * ITSE 1430
 * Patrick Fief
 * Lab 3
 */
using System;
using System.Linq;
using System.Windows.Forms;
using PatrickFief.MovieLib.Data;
using PatrickFief.MovieLib.Data.Memory;

namespace PatrickFief.MovieLib.Windows
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            RefreshUI();
        }

        #region Event Handlers

        private void OnCellDoubleClick( object sender, DataGridViewCellEventArgs e )
        {
            var movie = GetSelectedProduct();
            if (movie == null)
                return;

            EditMovie(movie);
        }

        //Called when a key is pressed while in a cell
        private void OnCellKeyDown( object sender, KeyEventArgs e )
        {
            var movie = GetSelectedProduct();
            if (movie == null)
                return;

            if (e.KeyCode == Keys.Delete)
            {
                e.Handled = true;
                DeleteMovie(movie);
            } else if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                EditMovie(movie);
            };
        }

        private void OnFileExit( object sender, EventArgs e )
        {
            Close();
        }

        private void OnMovieAdd( object sender, EventArgs e )
        {
            var button = sender as ToolStripMenuItem;

            var form = new MovieDetailForm("Add Product");

            //Show form modally
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //Add to database
            _database.Add(form.Movie, out var message);
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);

            RefreshUI();
        }

        private void OnMovieEdit( object sender, EventArgs e )
        {
            //Get selected product
            var movie = GetSelectedProduct();
            if (movie == null)
            {
                MessageBox.Show(this, "No movie selected", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };

            EditMovie(movie);
        }

        private void OnMovieDelete( object sender, EventArgs e )
        {
            //Get selected product
            var movie = GetSelectedProduct();
            if (movie == null)
            {
                MessageBox.Show(this, "No product selected", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };

            DeleteMovie(movie);
        }

        private void OnHelpAbout( object sender, EventArgs e )
        {
            var form = new MovieAboutForm();

            form.ShowDialog(this);
        }

        #endregion

        #region Private Members

        //Helper method to handle deleting movies
        private void DeleteMovie( Movie movie )
        {
            if (!ShowConfirmation("Are you sure?", "Remove Movie"))
                return;

            //Remove product
            _database.Remove(movie.Id);

            RefreshUI();
        }

        //Helper method to handle editing movies
        private void EditMovie( Movie movie )
        {
            var form = new MovieDetailForm(movie);
            var result = form.ShowDialog(this);
            if (result != DialogResult.OK)
                return;

            //Update the product
            form.Movie.Id = movie.Id;
            _database.Update(form.Movie, out var message);
            if (!String.IsNullOrEmpty(message))
                MessageBox.Show(message);

            RefreshUI();
        }

        private Movie GetSelectedProduct()
        {
            //TODO: Use the binding source
            //Get the first selected row in the grid, if any
            if (dataGridView1.SelectedRows.Count > 0)
                return dataGridView1.SelectedRows[0].DataBoundItem as Movie;

            return null;
        }

        private void RefreshUI()
        {
            //Get products
            var movies = _database.GetAll();

            //Bind to grid
            bindingSource1.DataSource = movies.ToList();
        }

        private bool ShowConfirmation( string message, string title )
        {
            return MessageBox.Show(this, message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                == DialogResult.Yes;
        }

        private Movie _movie;
        private IMovieDatabase _database = new MemoryProductDatabase();

        #endregion

        
    }
}
