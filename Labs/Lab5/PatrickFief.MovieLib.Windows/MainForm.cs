/*
 * ITSE 1430
 * Lab 5
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;
using PatrickFief.MovieLib.Data;
using PatrickFief.MovieLib.Data.Memory;
using PatrickFief.MovieLib.Data.Sql;

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

            var connString = ConfigurationManager.ConnectionStrings["MovieDatabase"];
            _database = new SqlMovieDatabase(connString.ConnectionString);

            RefreshUI();
        }

        #region Event Handlers

        //Called when a cell is double clicked
        private void OnCellDoubleClick( object sender, DataGridViewCellEventArgs e )
        {
            var movie = GetSelectedMovie();
            if (movie == null)
                return;
            
            EditMovie(movie);
        }

        //Called when a key is pressed while in a cell
        private void OnCellKeyDown( object sender, KeyEventArgs e )
        {
            var movie = GetSelectedMovie();
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

        private void OnMovieAdd ( object sender, EventArgs e )
        {
            var button = sender as ToolStripMenuItem;
            var form = new MovieDetailForm("Add Movie");
            var valid = false;

            do
            {
                //Show form modally
                var result = form.ShowDialog(this);
                if (result != DialogResult.OK)
                    return;

                //Add to database
                //_database.Add(form.Movie);
                try
                {
                    _database.Add(form.Movie);
                    valid = true;
                } catch (NotImplementedException)
                {
                    MessageBox.Show("not implemented yet");
                } catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    form = new MovieDetailForm(form.Movie);
                    form.Text = "Add Movie";
                };
            } while (!valid);

            RefreshUI();
        }

        private void OnMovieEdit( object sender, EventArgs e )
        {
            //Get selected movie
            var movie = GetSelectedMovie();
            if (movie == null)
            {
                MessageBox.Show(this, "No movie selected", "Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            };

            EditMovie(movie);            
        }

        private void OnMovieRemove( object sender, EventArgs e )
        {
            //Get selected movie
            var movie = GetSelectedMovie();
            if (movie == null)
            {
                MessageBox.Show(this, "No movie selected", "Error",
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

            //Remove movie
            try
            {
                _database.Remove(movie.Id);
            } catch (Exception e)
            {
                MessageBox.Show(e.Message);
            };

            RefreshUI();
        }

        //Helper method to handle editing movies
        private void EditMovie( Movie movie )
        {
            var form = new MovieDetailForm(movie);

            //Update the movie
            form.Movie.Id = movie.Id;
            var valid = false;

            do
            {
                //Show form modally
                var result = form.ShowDialog(this);
                if (result != DialogResult.OK)
                    return;
                
                try
                {
                    _database.Update(form.Movie);
                    valid = true;
                } catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                    form = new MovieDetailForm(form.Movie);
                };
            } while (!valid);

            RefreshUI();
        }

        private Movie GetSelectedMovie ( )
        {
            var items = (from r in dataGridView1.SelectedRows.OfType<DataGridViewRow>()
                         select new {
                             Index = r.Index,
                             Movie = r.DataBoundItem as Movie
                         }).FirstOrDefault();

            return items.Movie;
        }

        private void RefreshUI ()
        {
            //Get movies
            IEnumerable<Movie> movies = null;
            try
            {
                movies = _database.GetAll();
            } catch (Exception)
            {
                MessageBox.Show("Error loading movies");
            };

            movieBindingSource.DataSource = movies?.ToList();
        }

        private bool ShowConfirmation ( string message, string title )
        {
            return MessageBox.Show(this, message, title
                             , MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                           == DialogResult.Yes;
        }

        private IMovieDatabase _database;

        #endregion
    }
}
