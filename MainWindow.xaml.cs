/*
 * I. McTavish
 * March 26, 2018
 * This program uses a website and pulls information about the teams attending the North Bay FRC district event in 2018.  It writes all this data to a text file to use in other programs.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlueAlliance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //You need an api key from the Blue Alliance blog
        //see https://www.thebluealliance.com/apidocs for details
        string apiKey = "REPLACE_WITH_YOUR_OWN_KEY";

        public MainWindow()
        {
            InitializeComponent();
            //This program has no user interaction, it will simply run when the program opens.

            //webClient allows you to make http requests
            System.Net.WebClient webClient = new System.Net.WebClient();

            //You can find the addresses to use at https://www.thebluealliance.com/apidocs/v3
            //The base address allows you to create relative uris
            webClient.BaseAddress = "https://www.thebluealliance.com/api/v3/event/2018onnob/teams";

            //You need to add this header to be authorized to access the data
            webClient.Headers.Add("X-TBA-Auth-Key:" + apiKey);

            //The StreamReader class allows you to read from a data stream - in this case the http response.
            System.IO.StreamReader streamReader = new System.IO.StreamReader(webClient.OpenRead("https://www.thebluealliance.com/api/v3/event/2018onnob/teams"));

            //We will write to a file - this file will be in the same location as the .exe when this is run. Since this project is called BlueAlliance it is found in BlueAlliance\BlueAlliance\bin\Debug
            System.IO.StreamWriter streamWriter = new System.IO.StreamWriter("teams.txt");
            //Reading and writing files can cause errors - always use a try-catch statement
            try {

                streamWriter.Write(streamReader.ReadToEnd());
                //Flush forces that data to be written
                streamWriter.Flush();
                //Always close when done.
                streamWriter.Close();
                streamReader.Close();
                MessageBox.Show("All read");
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
