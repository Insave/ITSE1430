/*
 * ITSE 1430
 * Patrick Fief
 * Lab 3
 */
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace PatrickFief.MovieLib.Windows
{
    public partial class MovieDetailForm : Form
    {
        #region Construction
        /// <summary>A form to manipulate movies</summary>
        public MovieDetailForm()
        {
            InitializeComponent();
        }

        /// <summary>The movie detail form with a title</summary>
        /// <param name="title">The title of the form</param>
        public MovieDetailForm( string title ) : this()
        {
            Text = title;
        }

        /// <summary>The movie detail form with a predefined movie to allow editing</summary>
        /// <param name="movie">A movie object to edit</param>
        public MovieDetailForm(Movie movie) : this("Edit Movie")
        {
            Movie = movie;
        }
        #endregion

        /// <summary>The stored movie</summary>
        public Movie Movie { get; set; }

        protected override void OnLoad( EventArgs e )
        {
            base.OnLoad(e);

            //Load Movie
            if(Movie != null)
            {
                _txtTitle.Text = Movie.Title;
                _txtDescription.Text = Movie.Description;
                _txtLength.Text = Movie.Length.ToString();
                _chkIsOwned.Checked = Movie.IsOwned;
            }

            //Show the user required fields
            ValidateChildren();
        }

        private void OnSave( object sender, EventArgs e )
        {
            //Force validation of child controls
            if (!ValidateChildren())
                return;

            // Create product - using object initializer syntax
            var movie = new Movie() {
                Title = _txtTitle.Text,
                Description = _txtDescription.Text,
                Length = ConvertToLength(_txtLength),
                IsOwned = _chkIsOwned.Checked,
            };

            //Validate product using IValidatableObject
            var errors = ObjectValidator.Validate(movie);
            if (errors.Count() > 0)
            {
                //Get first error
                DisplayError(errors.ElementAt(0).ErrorMessage);
                return;
            };

            //Return from form
            Movie = movie;
            DialogResult = DialogResult.OK;

            Close();
        }

        private void DisplayError( string message )
        {
            MessageBox.Show(this, message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private int ConvertToLength( TextBox control )
        {
            if (String.IsNullOrEmpty(control.Text))
                return 0;
            if (Decimal.TryParse(control.Text, out var length))
                return (int)length;
            return -1;
        }

        private void _txtTitle_Validating( object sender, CancelEventArgs e )
        {
            var textbox = sender as TextBox;

            if (String.IsNullOrEmpty(textbox.Text))
            {
                _errorProvider.SetError(textbox, "Title is required");
                e.Cancel = true;
            } else
                _errorProvider.SetError(textbox, "");
        }

        private void _txtLength_Validating( object sender, CancelEventArgs e )
        {
            var textbox = sender as TextBox;

            var length = ConvertToLength(textbox);
            if (length < 0)
            {
                _errorProvider.SetError(textbox, "Length must be >= 0 minutes");
                e.Cancel = true;
            } else
                _errorProvider.SetError(textbox, "");
        }
    }
}
