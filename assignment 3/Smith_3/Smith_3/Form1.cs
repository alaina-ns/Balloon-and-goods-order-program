using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
// Assignment 3
// Programmer: Alaina Smith
// Project Description: Balloon order and goods application.
// Due Date: 4/8/2022
namespace Smith_3
{
    public partial class Form1 : Form
    {
        //Decalre class-level constans for tax rates and item rates
        private const decimal SALES_TAX = 0.07m;
        private const decimal SINGLE_ORDER = 9.95m;
        private const decimal HALF_DOZEN_ORDER = 35.95m;
        private const decimal DOZEN_ORDER = 65.95m;
        private const decimal EXTRAS = 9.50m;
        private const decimal MESSAGE = 2.50m;
        private const decimal STORE_PICK_UP = 0.00m;
        private const decimal HOME_DELIVERY = 7.50m;
        private const decimal STARTUP_BASE_PRICE = 0.0m;


        // Declare local variables 
        private decimal orderTotal;     
        private decimal subtotal = 0.00m;
        private decimal salesTaxAmount = 0.07m;
        string bundleSize = "";




        public Form1()
        {
            InitializeComponent();
        }

        // custom method to call occasions and extra into form1 text files
        private void PopulateBoxes()
        {
            try
            {
                // this opens text files
                StreamReader inputFile;
                inputFile = File.OpenText("Occassions.txt");
                while (!inputFile.EndOfStream)
                {
                    //then added into the combo box
                    occasionsComboBox.Items.Add(inputFile.ReadLine());
                }
                //closes the file
                inputFile.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                this.Close();
            }

            try  //opens file extra txt
            {
                //opens file extra txt
                StreamReader inputFile;
                inputFile = File.OpenText("Extras.txt");
                while (!inputFile.EndOfStream)
                {
                    //adds extra items into extralist box
                    extrasListBox.Items.Add(inputFile.ReadLine());
                }
                //closes the file
                inputFile.Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message); // Display mesage if error occurs when attempting to open file
                this.Close();
            }
        }

        // Execute upon program startup
        private void Form1_Load(object sender, EventArgs e)
        {
            // This sets birthday as default display for combo box
            occasionsComboBox.SelectedIndex = 0;
            // calls custom method to update the totals inside event handlers 
            UpdateTotals();
            //calls for text files such as extras and occassions
            PopulateBoxes();

            //assign in bundle size,and price labels
            storePickUpLabel.Text = STORE_PICK_UP.ToString("c");
            homeDeliveryLabel.Text = HOME_DELIVERY.ToString("c");
            singlePriceLabel.Text = SINGLE_ORDER.ToString("c");
            halfDozenPriceLabel.Text = HALF_DOZEN_ORDER.ToString("c");
            dozenPriceLabel.Text = DOZEN_ORDER.ToString("c");
            ExtraPriceLabel.Text = EXTRAS.ToString("c");
            personMessageLabel.Text = MESSAGE.ToString("c");
            orderTotalLabel.Text = STARTUP_BASE_PRICE.ToString("c");

            //Display current date (reported by system clock) in sale date control
            dateMaskedTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");

            // if else statement if the student picks single, half-dozen and dozen
            if (singleRadioButton.Checked)
                bundleSize = "Single";

            else if (halfDozenRadioButton.Checked)
                bundleSize = "Half-Dozen";

            else if (dozenRadioButton.Checked)
                bundleSize = "Dozen";

            //  if else statement  makes the messagebox invisible until its checked
            if (personalizedMessageCheckBox.Checked)
            {
                messageTextBox.Enabled = true;
                messageTextBox.Enabled = true;
            }
            else
            {
                messageTextBox.Enabled = false;
                messageTextBox.Enabled = false;
            }

            // if else statement makes othercheck box invisible until checked
            if (otherCheckBox.Checked)
            {
                titleComboBox.Enabled = false;
                otherTextBox.Enabled = true;
            }
            else
            {
                titleComboBox.Enabled = true;
                otherTextBox.Enabled = false;
            }
        }

        // Custom method to update total fields
        private void UpdateTotals()
        {
            // declare local variables
            string extraList = "";
            int extraCount = 0;

            subtotal = 0.00m;
            if (homeDeliveryRadioButton.Checked)
                subtotal += HOME_DELIVERY; // adds to the subtotal

            if (singleRadioButton.Checked)
                subtotal += SINGLE_ORDER; // adds to the subtotal

            else if (halfDozenRadioButton.Checked)
                subtotal += HALF_DOZEN_ORDER; // adds to the subtotal
            else
                subtotal += DOZEN_ORDER;
            // loop through all items in list box and write selected items to file
            for (int counter =0; counter < extrasListBox.Items.Count; counter++)
            {
                // get selected method to determine if list box item is selected or not
                if (extrasListBox.GetSelected(counter))
                {
                    // square brackets signify an array
                    extraList += extrasListBox.Items[counter] + "\n";
                    extraCount++; // increment count of selected items
                    subtotal += EXTRAS;
                }
            }

            // calculate subtotal amount with sales tax + message
            if (personalizedMessageCheckBox.Checked)
                subtotal += MESSAGE;

            salesTaxAmount = subtotal * SALES_TAX;
            orderTotal = subtotal + salesTaxAmount;

            //assign in subtotal,salesamount and ordertotal
            subTotalLabel.Text = subtotal.ToString("c");
            salesTaxAmountLabel.Text = salesTaxAmount.ToString("c");
            orderTotalLabel.Text = orderTotal.ToString("c");
        }
        // Handle update store pickup button click event
        private void StorePickUpRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // if else statement updates total when button triggered
            if (singleRadioButton.Checked)
            {
                UpdateTotals();
            }
            // if else statement updates total when button triggered
            else if (halfDozenRadioButton.Checked)
            {
                UpdateTotals();
            }
            // if else statement updates total when button triggered
            else if (dozenRadioButton.Checked)
            {
                UpdateTotals();
            }

        }
        // Handle update home delivery button click event
        private void homeDeliveryRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // if else statement updates total when button triggered
            if (singleRadioButton.Checked)
               {
                UpdateTotals(); // Call custom method to update calculated totals
            }
            // if else statement updates total when button triggered
            else if (halfDozenRadioButton.Checked)
              {
                UpdateTotals(); // Call custom method to update calculated totals
            }
            // if else statement updates total when button triggered
            else if (dozenRadioButton.Checked)
               {
                UpdateTotals(); // Call custom method to update calculated totals
            }
        }
        // Handle update extralist button click event
        private void ExtrasListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

            UpdateTotals(); // Call custom method to update calculated totals

        }

        private void SingleRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotals(); // Call custom method to update calculated totals

        }
        private void HalfDozenRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotals();  // Call custom method to update calculated totals
        }

        private void DozenRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotals(); // Call custom method to update calculated totals

        }
        // Handle update othercheck button click event
        private void otherCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // if else statement updates total when button triggered
            if (otherCheckBox.Checked)
            {
                titleComboBox.Enabled = false;
                otherTextBox.Enabled = true;
            }
            else
            {
                titleComboBox.Enabled = true;
                otherTextBox.Enabled = false;
            }
        }
        // Handle update person message button click event
        private void personalizedMessageCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateTotals(); // Call custom method to update calculated totals

            // This makes the messagebox invisible until its checked
            if (personalizedMessageCheckBox.Checked)
            {
                messageTextBox.Enabled = true;
                messageTextBox.Enabled = true;
            }
            else
            {
                messageTextBox.Enabled = false;
                messageTextBox.Enabled = false;
            }

        }
        // Custom method to reset the form
        private void ResetForm()
        {
            // This clears all the text, buttons, commands, etc
            otherTextBox.Text = "";
            storePickUpRadioButton.Checked = true;
            singleRadioButton.Checked = true;
            homeDeliveryRadioButton.Checked = false;
            otherCheckBox.Checked = false;
            personalizedMessageCheckBox.Checked = false;
            //this cancels out the items as 0 counts as a index
            occasionsComboBox.SelectedIndex = -1;
            extrasListBox.SelectedIndex = -1;
            titleComboBox.SelectedIndex = -1;
            firstNameTextBox.Text = "";
            lastNameTextBox.Text = "";
            StreetTextBox.Text = "";
            cityMaskedTextBox.Text = "";
            zipMaskedTextBox.Text = "";
            stateTextBox.Text = "";
            phoneMaskedTextBox.Text = "";
            messageTextBox.Text = "";
            personalizedMessageCheckBox.Checked = false;
            orderTotalLabel.Text = "";
            subTotalLabel.Text = "";
            salesTaxAmountLabel.Text = "";
            //send focus to first date entry on form
            storePickUpRadioButton.Focus();
        }

        // Handle update display summary button click event
        private void displaySummaryButton_Click(object sender, EventArgs e)
        {
            string extraList = "";
            
            for(int counter= 0; counter < extrasListBox.Items.Count; counter++)
            {
                if(extrasListBox.GetSelected(counter))
                {
                    extraList += extrasListBox.Items[counter] + "\n";
                }
            }

            // if else statement that requires user to input all three: first, last name and phone number
                if (firstNameTextBox.Text == "" || lastNameTextBox.Text == "" || phoneMaskedTextBox.Text == "")
                {
                // Statement saying you need all three required text inputs
                MessageBox.Show("To save your order, you must include your first and last name, as well as your phone number.",
                "Summary Order ERROR", MessageBoxButtons.OK,
                 MessageBoxIcon.Exclamation);

                 }
            else
            {
                // Displays "Order summary of what the customer selected
                MessageBox.Show("Bonnie’s Balloons \n\n" +
               "Title:" + titleComboBox.Text + otherTextBox.Text + "\n" + "First Name: " + firstNameTextBox.Text + "\n"
               + "Last Name:" + lastNameTextBox.Text + "\n" +
               "Street" + StreetTextBox.Text + "\n" + "City:" + cityMaskedTextBox.Text + "\n" +
               "State" + stateTextBox.Text + "\n" + "Zipcode:" + zipMaskedTextBox.Text + "\n" +
                 "Phone Number:" + phoneMaskedTextBox.Text + "\n" + "Delivery Date:" + dateMaskedTextBox.Text + "\n" + "Bundle Size:" + bundleSize + "\n" + "Ocassions:" + occasionsComboBox.Text + "\n" +
                 "Extras:" + extraList + "Custom message:" + messageTextBox.Text + "\n" + "Subtotal:" + subTotalLabel.Text + "\n" + "Sales tax amount:" + salesTaxAmountLabel.Text + "\n" + "Order Total:" + orderTotalLabel.Text + "\n",
                 "Order Summary", MessageBoxButtons.OK,
                 MessageBoxIcon.Information);
     


            }
                // this resets form after displaying the order summary
            ResetForm();
        }

        //// Clear data entry controls and reset form to initial set up
        private void ClearButton_Click(object sender, EventArgs e)
        {
            // this resets form 
            ResetForm();
        }
        // Allows for only alphabetic characters
        private void firstNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }
        // Allows for only alphabetic characters
        private void lastNameTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }
        // Allows for only alphabetic characters
        private void cityMaskedTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
                e.Handled = true;
        }
        // event handler for exit button
        private void ExitButton_Click(object sender, EventArgs e)
        {

            // Declare variable to hold user selection in dialog box
            DialogResult selection;
            selection = MessageBox.Show("Are you sure you wish to quit?",
            "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            // Take appropriate action based on user selection in dialog box
            if (selection == DialogResult.Yes)
            {
                // closes the program
                this.Close();
            }
        }

    }
}
