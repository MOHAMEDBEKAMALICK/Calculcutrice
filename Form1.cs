using System;
using System.Linq;
using System.Windows.Forms;

namespace Calculator_windows
{
   
    public partial class Form1 : Form
    {
      
        public Form1()
        {
            InitializeComponent();
        }

      
        private void CEButton_Click(object sender, EventArgs e)
        {
            // Clears the text from the user input text box
            this.UserInputText.Text = string.Empty;

            // Focus the user input text
            FocusInputText();
        }

       
        private void DelButton_Click(object sender, EventArgs e)
        {
            // Delete the value after the selected position
            DeleteTextValue();

            // Focus the user input text
            FocusInputText();
        }

    
        private void DivideButton_Click(object sender, EventArgs e)
        {
            // Insert the value in the user input text box at the currently selected position
            InsertTextValue("/");

            // Focus the user input text
            FocusInputText();
        }

      
        private void TimesButton_Click(object sender, EventArgs e)
        {
            // Insert the value in the user input text box at the currently selected position
            InsertTextValue("*");

            // Focus the user input text
            FocusInputText();
        }

      
        private void MinusButton_Click(object sender, EventArgs e)
        {
            // Insert the value in the user input text box at the currently selected position
            InsertTextValue("-");

            // Focus the user input text
            FocusInputText();
        }

       
        private void PlusButton_Click(object sender, EventArgs e)
        {
            // Insert the value in the user input text box at the currently selected position
            InsertTextValue("+");

            // Focus the user input text
            FocusInputText();
        }

      
        private void EqualsButton_Click(object sender, EventArgs e)
        {
            // Calculate the equation
            CalculateEquation();

            // Focus the user input text
            FocusInputText();
        }

       
        private void DotButton_Click(object sender, EventArgs e)
        {
            // Insert the value in the user input text box at the currently selected position
            InsertTextValue(".");

            // Focus the user input text
            FocusInputText();
        }

        private void ZeroButton_Click(object sender, EventArgs e)
        {
            // Insert the value in the user input text box at the currently selected position
            InsertTextValue("0");

            // Focus the user input text
            FocusInputText();
        }

      
        private void OneButton_Click(object sender, EventArgs e)
        {
            // Insert the value in the user input text box at the currently selected position
            InsertTextValue("1");

            // Focus the user input text
            FocusInputText();
        }

      
        private void TwoButton_Click(object sender, EventArgs e)
        {
            // Insert the value in the user input text box at the currently selected position
            InsertTextValue("2");

            // Focus the user input text
            FocusInputText();
        }

       
        private void ThreeButton_Click(object sender, EventArgs e)
        {
            // Insert the value in the user input text box at the currently selected position
            InsertTextValue("3");

            // Focus the user input text
            FocusInputText();
        }

     
        private void FourButton_Click(object sender, EventArgs e)
        {
            // Insert the value in the user input text box at the currently selected position
            InsertTextValue("4");

            // Focus the user input text
            FocusInputText();
        }

        /// <summary>
        /// Adds the 5 character to the text at the currently selection position
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void FiveButton_Click(object sender, EventArgs e)
        {
            // Insert the value in the user input text box at the currently selected position
            InsertTextValue("5");

            // Focus the user input text
            FocusInputText();
        }

      
        private void SixButton_Click(object sender, EventArgs e)
        {
            // Insert the value in the user input text box at the currently selected position
            InsertTextValue("6");

            // Focus the user input text
            FocusInputText();
        }

      
        private void SevenButton_Click(object sender, EventArgs e)
        {
            // Insert the value in the user input text box at the currently selected position
            InsertTextValue("7");

            // Focus the user input text
            FocusInputText();
        }

      
        private void EightButton_Click(object sender, EventArgs e)
        {
            // Insert the value in the user input text box at the currently selected position
            InsertTextValue("8");

            // Focus the user input text
            FocusInputText();
        }

    
        private void NineButton_Click(object sender, EventArgs e)
        {
            // Insert the value in the user input text box at the currently selected position
            InsertTextValue("9");

            // Focus the user input text
            FocusInputText();
        }

        #endregion

        private void CalculateEquation()
        {
       

            this.CalculationResultText.Text = ParseOperation();

            // Focus the user input text
            FocusInputText();
        }

    
        private string ParseOperation()
        {
            try
            {
                // Get the users equation input
                var input = this.UserInputText.Text;

                // Remove all spaces
                input = input.Replace(" ", "");

                // Create a new top-level operation
                var operation = new Operation();
                var leftSide = true;

                // Loop through each character of the input
                // starting from the left working to the right
                for (int i = 0; i < input.Length; i++)
                {
                  

                    // Check if the current character is a number
                    if ("0123456789.".Any(c => input[i] == c))
                    {
                        if (leftSide)
                            operation.LeftSide = AddNumberPart(operation.LeftSide, input[i]);
                        else
                            operation.RightSide = AddNumberPart(operation.RightSide, input[i]);
                    }
                    // If it is an operator ( + - * / ) set the operator type
                    else if ("+-*/".Any(c => input[i] == c))
                    {
                        // If we are on the right side already, we now need to calculate our current operation
                        // and set the result to the left side of the next operation
                        if (!leftSide)
                        {
                            // Get the operator type
                            var operatorType = GetOperationType(input[i]);

                            // Check if we actually have a right side number
                            if (operation.RightSide.Length == 0)
                            {
                                // Check the operator is not a minus (as they could be creating a negative number)
                                if (operatorType != OperationType.Minus)
                                    throw new InvalidOperationException($"Operator (+ * / or more than one -) specified without an right side number");

                                // If we got here, the operator type is a minus, and there is no left number currently, so add the minus to the number
                                operation.RightSide += input[i];
                            }
                            else
                            {
                                // Calculate previous equation and set to the left side
                                operation.LeftSide = CalculateOperation(operation);

                                // Set new operator
                                operation.OperationType = operatorType;

                                // Clear the previous right number
                                operation.RightSide = string.Empty;
                            }
                        }
                        else
                        {
                            // Get the operator type
                            var operatorType = GetOperationType(input[i]);

                            // Check if we actually have a left side number
                            if (operation.LeftSide.Length == 0)
                            {
                                // Check the operator is not a minus (as they could be creating a negative number)
                                if (operatorType != OperationType.Minus)
                                    throw new InvalidOperationException($"Operator (+ * / or more than one -) specified without an left side number");

                                // If we got here, the operator type is a minus, and there is no left number currently, so add the minus to the number
                                operation.LeftSide += input[i];
                            }
                            else
                            {
                                // If we get here, we have a left number and now an operator, so we want to move to the right side

                                // Set the operation type
                                operation.OperationType = operatorType;

                                // Move to the right side
                                leftSide = false;
                            }
                        }
                    }
                }

                // If we are done parsing, and there were no exceptions
                // calculate the current operation
                return CalculateOperation(operation);
            }
            catch (Exception ex)
            {
                return $"Invalid equation. {ex.Message}";
            }
        }

       
        private string CalculateOperation(Operation operation)
        {
            // Store the number values of the string representations
            decimal left = 0;
            decimal right = 0;

            // Check if we have a valid left side number
            if (string.IsNullOrEmpty(operation.LeftSide) || !decimal.TryParse(operation.LeftSide, out left))
                throw new InvalidOperationException($"Left side of the operation was not a number. {operation.LeftSide}");

            // Check if we have a valid right side number
            if (string.IsNullOrEmpty(operation.RightSide) || !decimal.TryParse(operation.RightSide, out right))
                throw new InvalidOperationException($"Right side of the operation was not a number. {operation.RightSide}");

            try
            {
                switch (operation.OperationType)
                {
                    case OperationType.Add:
                        return (left + right).ToString();
                    case OperationType.Minus:
                        return (left - right).ToString();
                    case OperationType.Divide:
                        return (left / right).ToString();
                    case OperationType.Multiply:
                        return (left * right).ToString();
                    default:
                        throw new InvalidOperationException($"Unknown operator type when calculating operation. {operation.OperationType}");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to calculate operation {operation.LeftSide} {operation.OperationType} {operation.RightSide}. {ex.Message}");
            }
        }

        private OperationType GetOperationType(char character)
        {
            switch (character)
            {
                case '+':
                    return OperationType.Add;
                case '-':
                    return OperationType.Minus;
                case '/':
                    return OperationType.Divide;
                case '*':
                    return OperationType.Multiply;
                default:
                    throw new InvalidOperationException($"Unknown operator type {character}");
            }
        }

     
        private string AddNumberPart(string currentNumber, char newCharacter)
        {
            // Check if there is already a . in the number
            if (newCharacter == '.' && currentNumber.Contains('.'))
                throw new InvalidOperationException($"Number {currentNumber} already contains a . and another cannot be added");

            return currentNumber + newCharacter;
        }
    }
}