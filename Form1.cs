using FinalProject__Laundry_.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;



namespace FinalProject__Laundry_
{
    public partial class Form1 : Form
    {
        public static readonly HttpClient client = new HttpClient();
        public Form1()
        {
            InitializeComponent();
        }
        private async Task<List<LaundryModel>> GetLaundryModelsAsync()
        {
            try
            {
                string url = "http://localhost:5193/api/laundrymodels"; // Replace with your actual API endpoint
                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var jsonResponse = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<LaundryModel>>(jsonResponse);
                }
                else
                {
                    MessageBox.Show("Failed to retrieve data.");
                    return new List<LaundryModel>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
                return new List<LaundryModel>();
            }
        }
        private async Task CreateLaundryRecordAsync(LaundryModel model)
        {
            try
            {
                string url = "http://localhost:5193/api/laundrymodels";
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Laundry record created successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to create laundry record.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task UpdateLaundryRecordAsync(int id, LaundryModel model)
        {
            try
            {
                string url = $"http://localhost:5193/api/laundrymodels/{id}";
                var jsonContent = JsonConvert.SerializeObject(model);
                var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Laundry record updated successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to update laundry record.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task DeleteLaundryRecordAsync(int id)
        {
            try
            {
                string url = $"http://localhost:5193/api/laundrymodels/{id}";
                HttpResponseMessage response = await client.DeleteAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Laundry record deleted successfully.");
                }
                else
                {
                    MessageBox.Show("Failed to delete laundry record.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private async Task RefreshDataGridView()
        {
            var laundryRecords = await GetLaundryModelsAsync(); // Call the correct method to get records
            dataGridView1.DataSource = laundryRecords;
        }
        private async void btnGet_Click(object sender, EventArgs e)
        {
            await RefreshDataGridView(); // Call the async refresh method
        }

        private void btnPost_Click(object sender, EventArgs e)
        {
          
        }

        private void btnPut_Click(object sender, EventArgs e)
        {

        }

        private void btnGetAll_Click(object sender, EventArgs e)
        {

        }

        private void btnDel_Click(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
