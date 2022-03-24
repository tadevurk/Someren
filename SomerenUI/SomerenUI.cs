using SomerenLogic;
using SomerenModel;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SomerenDAL;
using System.Drawing;

namespace SomerenUI
{
    public partial class SomerenUI : Form
    {
        DrinkService drinkService;
        RoomService roomService;
        StudentService studentService;
        TeacherService teacherService;
        RevenueReportService revenueReportService;
        CashRegisterService cashRegisterService;
        ActivityService activityService;
        //Vedat
        ActivityStudentService ActivityStudentService;

        public SomerenUI()
        {
            InitializeComponent();
            drinkService = new DrinkService();
            roomService = new RoomService();
            studentService = new StudentService();
            teacherService = new TeacherService();
            revenueReportService = new RevenueReportService();
            cashRegisterService = new CashRegisterService();
            activityService = new ActivityService();
        }

        private void SomerenUI_Load(object sender, EventArgs e)
        {
            showPanel("Dashboard");
            cmbAmount.SelectedIndex = 0;
        }

        //Panel controles & Read list
        private void showPanel(string panelName)
        {
            try
            {
                //Hide all panels before opening the panel that you want to open
                pnlDashboard.Hide();
                imgDashboard.Hide();
                pnlStudents.Hide();
                pnlTeachers.Hide();
                pnlRooms.Hide();
                pnlActivities.Hide();
                pnlDrinks.Hide();
                pnlCashRegister.Hide();
                pnlRevenueReport.Hide();
                pnlActivityStudents.Hide();
                lbl_Dashboard.Visible = false;


                switch (panelName)//devide the cases into the following parameters.
                {// make enum with the names of panels
                    case "Dashboard":
                        pnlDashboard.Show();
                        imgDashboard.Show();
                        lbl_Dashboard.Visible = true;
                        break;

                    case "Students":
                        pnlDashboard.Show();
                        pnlStudents.Show();

                        ReadListOfStudents();
                        break;

                    case "Teachers":
                        pnlDashboard.Show();
                        pnlTeachers.Show();

                        ReadListOfTeachers();
                        break;

                    case "Activities":
                        pnlDashboard.Show();
                        pnlActivities.Show();
                        TextBoxActivitiesClear();
                        ReadListOfActivities();
                        break;

                    case "Rooms":
                        pnlDashboard.Show();
                        pnlRooms.Show();

                        ReadListOfRooms();
                        break;

                    case "Drinks":
                        pnlDashboard.Show();
                        pnlDrinks.Show();
                        TextBoxDrinkClear();
                        ReadListOfDrinks();
                        break;

                    case "CashRegister":
                        pnlDashboard.Show();
                        pnlCashRegister.Show();

                        ReadListOfStudentsCashRegister();
                        ReadListOfDrinksCashRegister();
                        break;

                    case "Revenue Report":
                        pnlDashboard.Show();
                        pnlRevenueReport.Show();
                        ReportClear();
                        break;
                    case "Activities For Students":
                        pnlDashboard.Show();
                        pnlActivityStudents.Show();
                        DisplayActivites();
                        EnableButtons();
                        break;
                }
            }
            catch (Exception e)
            {
                BaseDao.ErrorLogging(e);

                MessageBox.Show($"Something went wrong while loading the {panelName}: " + e.Message);
            }
        }
        private void ReadListOfStudents()
        {
            // fill the students listview within the students panel with a list of students
            List<Student> studentList = studentService.GetStudents(); ;

            // clear the listview before filling it again
            listViewStudents.Items.Clear();
            listViewStudents.FullRowSelect = true;

            foreach (Student s in studentList)
            {
                ListViewItem li = new ListViewItem(s.Number.ToString());
                li.SubItems.Add(s.FirstName);
                li.SubItems.Add(s.LastName);
                //take the only date values from the BirthDate, leaving time.
                li.SubItems.Add(s.BirthDate.ToString("dd/MM/yyyy"));

                listViewStudents.Items.Add(li);
            }
        }
        private void ReadListOfTeachers()
        {
            List<Teacher> teacherList = teacherService.GetTeachers();

            listViewTeachers.Items.Clear();
            listViewTeachers.FullRowSelect = true;


            foreach (Teacher t in teacherList)
            {
                ListViewItem li2 = new ListViewItem(t.Number.ToString());
                li2.SubItems.Add(t.FirstName);
                li2.SubItems.Add(t.LastName);
                //change supervisor values into the supervior and not.
                if (t.Supervisor)
                    li2.SubItems.Add("Supervisor");
                else
                    li2.SubItems.Add("not Supervisor");

                listViewTeachers.Items.Add(li2);
            }
        }
        private void ReadListOfActivities()
        {
            List<Activity> activitiesList = activityService.GetActivities();

            listViewActivities.Items.Clear();
            listViewActivities.FullRowSelect = true;

            foreach (Activity a in activitiesList)
            {
                ListViewItem li = new ListViewItem(a.ActivityId.ToString());
                li.SubItems.Add(a.ActivityName);
                //take the only date and time values from the ActivityDateTime, leaving miliseconds etc.
                li.SubItems.Add(a.ActivityDateTime.ToString("dd/MM/yyyy H:mm"));

                listViewActivities.Items.Add(li);
            }
        }
        private void ReadListOfRooms()
        {
            List<Room> roomList = roomService.GetRooms(); ;

            listViewRooms.Items.Clear();
            listViewRooms.FullRowSelect = true;

            foreach (Room r in roomList)
            {
                ListViewItem li = new ListViewItem(r.Number.ToString());
                //change room type values into the teacher room and student room.
                if (r.Type)
                {
                    li.SubItems.Add("Teacher room");
                }
                else
                {
                    li.SubItems.Add("Student room");
                }
                li.SubItems.Add(r.Capacity.ToString());

                listViewRooms.Items.Add(li);
            }
        }
        private void ReadListOfDrinks()
        {
            List<Drink> drinkList = drinkService.GetDrinks();

            listViewDrinks.Items.Clear();
            listViewDrinks.FullRowSelect = true;
            listViewDrinks.Columns[0].DisplayIndex = 5;

            foreach (Drink d in drinkList)
            {
                ListViewItem li = new ListViewItem();
                if (d.Stock < 10)
                {
                    string fileLocation = @"..\..\..\icon.jpg";
                    ImageList photoList = new ImageList();
                    photoList.ImageSize = new Size(20, 20);
                    listViewDrinks.SmallImageList = photoList;
                    photoList.Images.Add(Image.FromFile(fileLocation));
                    li.ImageIndex = 0;
                }
                //set the icon in the 5th column

                li.SubItems.Add(d.Id.ToString());
                li.SubItems.Add(d.Name.ToString());
                if (d.Type)
                {
                    li.SubItems.Add("Alcohole");
                }
                else
                {
                    li.SubItems.Add("Non-Alcohole");
                }
                li.SubItems.Add(d.Price.ToString("0.00"));
                li.SubItems.Add(d.Stock.ToString());
                li.Tag = d;
                listViewDrinks.Items.Add(li);
            }
        }
        private void ReadListOfStudentsCashRegister()
        {
            // fill the students listview within the students panel with a list of students
            List<Student> studentList = studentService.GetStudents();

            // clear the listview before filling it again
            listViewStudentsCashRegister.Items.Clear();
            listViewStudentsCashRegister.FullRowSelect = true;

            foreach (Student s in studentList)
            {
                ListViewItem li = new ListViewItem(s.Number.ToString());
                li.SubItems.Add(s.FirstName);
                li.SubItems.Add(s.LastName);

                listViewStudentsCashRegister.Items.Add(li);
            }
        }
        private void ReadListOfDrinksCashRegister()
        {
            // fill the students listview within the students panel with a list of students
            DrinkService drinkService = new DrinkService(); ;
            List<Drink> drinkList = drinkService.GetDrinks(); ;

            // clear the listview before filling it again
            listViewDrinksCashRegister.Items.Clear();
            listViewDrinksCashRegister.FullRowSelect = true;

            foreach (Drink d in drinkList)
            {
                ListViewItem li = new ListViewItem(d.Id.ToString());
                li.SubItems.Add(d.Name);
                //change alcohole values into the alcoholic and not.
                if (d.Type)
                {
                    li.SubItems.Add("Alcohole");
                }
                else
                {
                    li.SubItems.Add("Non-Alcohole");
                }
                li.SubItems.Add(d.Price.ToString("f2"));
                li.SubItems.Add(d.Stock.ToString());
                li.Tag = d;

                listViewDrinksCashRegister.Items.Add(li);
            }
        }


        //Main screen
        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void imgDashboard_Click(object sender, EventArgs e)
        {
            MessageBox.Show("What happens in Someren, stays in Someren!");
        }

        //Menu controles
        private void dashboardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showPanel("Dashboard");
        }
        private void studentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Students");
        }
        private void teachersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Teachers");
        }
        private void roomsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Rooms");
        }
        private void activitiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Activities");
        }
        private void drinksManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Drinks");
        }
        private void cashRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("CashRegister");
        }
        private void revenueReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Revenue Report");
        }
        private void activitiesForStudentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showPanel("Activities For Students");
        }

        //Drink management functions
        private void listViewDrinks_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            try
            {
                if (listViewDrinks.SelectedItems.Count == 0)
                {
                    return;
                }
                Drink selectedDrink = (Drink)listViewDrinks.SelectedItems[0].Tag;
                txtName.Text = selectedDrink.Name;
                txtPrice.Text = selectedDrink.Price.ToString("0.00");
                txtStock.Text = selectedDrink.Stock.ToString();
                if (selectedDrink.Type)
                {
                    cmbType.SelectedIndex = 1;//Alcohole
                }
                else
                {
                    cmbType.SelectedIndex = 2;//non-alcohole
                }
                btnDelete.Enabled = true;
                btnUpdate.Enabled = true;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }
        private void TextBoxDrinkClear()//initialize the text box
        {
            btnDelete.Enabled = false;
            btnUpdate.Enabled = false;

            txtName.Clear();
            txtPrice.Clear();
            txtStock.Clear();
            cmbType.SelectedIndex = 0;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //check if all of necessary field is filled up, otherwise throw an exception.
            if (string.IsNullOrEmpty(txtName.Text) || string.IsNullOrEmpty(txtPrice.Text) || string.IsNullOrEmpty(txtStock.Text))
            {
                MessageBox.Show("Please type all of mendatory field", "ERROR");
                return;
            }
            Drink newDrink = new Drink();
            newDrink.Id = 0;
            newDrink.Name = txtName.Text;
            if (cmbType.SelectedIndex == 1)
            {
                newDrink.Type = true;
            }
            else if (cmbType.SelectedIndex == 2)
            {
                newDrink.Type = false;
            }
            newDrink.Price = decimal.Parse(txtPrice.Text);
            newDrink.Stock = int.Parse(txtStock.Text);
            drinkService.Add(newDrink);
            TextBoxDrinkClear();
            showPanel("Drinks");
            MessageBox.Show("New drink is added!", "SUCCESS");
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewDrinks.SelectedItems.Count > 0)//check if an item selected.
                {
                    Drink deleteDrink = (Drink)listViewDrinks.SelectedItems[0].Tag;
                    drinkService.Delete(deleteDrink);
                    TextBoxDrinkClear();
                    showPanel("Drinks");
                    MessageBox.Show("The drink is deleted!", "SUCCESS");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewDrinks.SelectedItems[0] == null)
                {
                    return;
                }
                Drink updatedDrink = (Drink)listViewDrinks.SelectedItems[0].Tag;
                updatedDrink.Name = txtName.Text;
                updatedDrink.Price = decimal.Parse(txtPrice.Text);
                updatedDrink.Type = cmbType.SelectedIndex == 1;//Type =true -> Index =1
                updatedDrink.Stock = int.Parse(txtStock.Text);

                drinkService.Update(updatedDrink);
                TextBoxDrinkClear();//clear text box
                showPanel("Drinks");//refresh panel
                MessageBox.Show("The drink is updated!", "SUCCESS");
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        //Cash Register functions
        private void btnCalculate_Click_1(object sender, EventArgs e)
        {
            if (listViewDrinksCashRegister.SelectedItems.Count > 0)
            {
                int amount = int.Parse(cmbAmount.SelectedItem.ToString());
                decimal priceToPay = 0;

                if (amount > int.Parse(listViewDrinksCashRegister.SelectedItems[0].SubItems[4].Text))
                {
                    MessageBox.Show("Please enter a valid amount!, Invalid Amount", "ERROR");
                }
                else if (amount == 0)
                {
                    MessageBox.Show("Please enter an amount.", "ERROR");
                }
                else
                {
                    priceToPay = amount * decimal.Parse(listViewDrinksCashRegister.SelectedItems[0].SubItems[3].Text);
                }
                txtTotal.Text = $"{priceToPay: 0.00}";
                //unused line are removed.
            }
            else
            {
                MessageBox.Show("Please select the student and drink first.", "ERROR");
            }
        }
        private void btnCheckOut_Click_1(object sender, EventArgs e)
        {
            if (listViewDrinksCashRegister.SelectedItems.Count > 0 && listViewStudentsCashRegister.SelectedItems.Count > 0)
            {
                //make a sales record
                Sale s = new Sale();
                s.SaleId = 0;
                s.StudentId = int.Parse(listViewStudentsCashRegister.SelectedItems[0].SubItems[0].Text);
                s.DrinkId = int.Parse(listViewDrinksCashRegister.SelectedItems[0].SubItems[0].Text);
                s.SaleCount = int.Parse(cmbAmount.SelectedItem.ToString());
                s.TotalPayment = decimal.Parse(txtTotal.Text);
                s.SaleDate = DateTime.Now;

                //update drink stock 
                Drink selectedDrink = (Drink)listViewDrinksCashRegister.SelectedItems[0].Tag;
                selectedDrink.Stock -= s.SaleCount;

                //set the textbox empty
                cmbAmount.SelectedIndex = 0;
                txtTotal.Clear();

                if (selectedDrink.Stock < 0)// go back if the stock is not enough
                {
                    MessageBox.Show("Stock is not enough, choose another item", "ERROR");
                    return;
                }

                //excute to add the sale and update current stock.
                cashRegisterService.AddSale(s);
                drinkService.Update(selectedDrink);

                txtTotal.Clear();
                cmbAmount.SelectedIndex = 0;

                ReadListOfDrinksCashRegister();
                ReadListOfStudentsCashRegister();
            }
            else
            {
                MessageBox.Show("To checkout, you must choose student and drink first, then you must enter an amount.", "ERROR");
            }
        }

        //Revenue Report functions
        private void btnGetReport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtpDateFrom.Value.Date > dtpDateTo.Value.Date)
                {
                    MessageBox.Show("YOU CANNOT GET REPORT WITH INVALID DATE!", "ERROR");
                }
                else if (dtpDateFrom.Value.Date > DateTime.Now.Date || dtpDateTo.Value.Date > DateTime.Now.Date)
                {
                    MessageBox.Show("YOU CANNOT CHOOSE A FUTURE DATE!", "ERROR");
                }
                else
                {
                    RevenueReport revenueReport = revenueReportService.GetSales(dtpDateFrom.Value.Date, dtpDateTo.Value.Date);
                    lblSalesReport.Text = revenueReport.NumberOfSales.ToString();
                    lblTotalCustomer.Text = revenueReport.NumberOfCustomers.ToString();
                    lblTurnOver.Text = $" € {revenueReport.TurnOver.ToString("00.00")}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }
        private void ReportClear()//initialize the text box
        {
            lblSalesReport.Text = "0";
            lblTurnOver.Text = "0";
            lblTotalCustomer.Text = "0";
        }

        //
        //
        //4th Assignment Variant A: List Of Activities        

        private void listViewActivities_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listViewActivities.SelectedItems.Count == 0)
                {
                    return;
                }

                //txtNewActivityName.Text = listViewActivities.SelectedItems[0].SubItems[1].Text;
                //datePickerActivities.Value = listViewActivities.SelectedItems[0].SubItems[2].               

                btnChangeActivity.Enabled = true;
                btnRemoveActivity.Enabled = true;
                btnAddActivity.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void TextBoxActivitiesClear()
        {
            btnChangeActivity.Enabled = false;
            btnRemoveActivity.Enabled = false;
            btnAddActivity.Enabled = true;

            txtNewActivityName.Clear();
            datePickerActivities.Refresh();
            timePickerActivities.Refresh();
        }

        private void btnAddActivity_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNewActivityName.Text))
            {
                MessageBox.Show("Please enter an activity name and select a datetime.", "ERROR");
            }
            else
            {
                DateTime activityTime = datePickerActivities.Value.Date + timePickerActivities.Value.TimeOfDay;

                Activity newActivity = new Activity();
                newActivity.ActivityId = 0;
                newActivity.ActivityName = txtNewActivityName.Text;
                newActivity.ActivityDateTime = activityTime;

                activityService.AddActivity(newActivity);

                TextBoxActivitiesClear();
                showPanel("Activities");

                MessageBox.Show("Activity has been added.", "SUCCESS");
            }
        }

        private void btnChangeActivity_Click(object sender, EventArgs e)
        {
            try
            {
                if (listViewActivities.SelectedItems[0] == null)
                {
                    return;
                }

                //processing the change

                if (string.IsNullOrEmpty(txtNewActivityName.Text))
                {
                    MessageBox.Show("Please enter an activity name.", "ERROR");
                }
                else
                {
                    DateTime activityTime = datePickerActivities.Value.Date + timePickerActivities.Value.TimeOfDay;

                    Activity changedActivity = new Activity();

                    changedActivity.ActivityId = int.Parse(listViewActivities.SelectedItems[0].SubItems[0].Text);
                    changedActivity.ActivityName = txtNewActivityName.Text;
                    changedActivity.ActivityDateTime = activityTime;

                    activityService.ChangeActivity(changedActivity);

                    TextBoxActivitiesClear();
                    showPanel("Activities");

                    MessageBox.Show("Activity has been changed.", "SUCCESS");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void btnRemoveActivity_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dialogResult = MessageBox.Show("Are you sure you wish to remove this activity?", "CONFIRMATION", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    Activity removedActivity = new Activity();

                    removedActivity.ActivityId = int.Parse(listViewActivities.SelectedItems[0].SubItems[0].Text);
                    removedActivity.ActivityName = listViewActivities.SelectedItems[0].SubItems[1].Text;
                    removedActivity.ActivityDateTime = Convert.ToDateTime(listViewActivities.SelectedItems[0].SubItems[2].Text);

                    activityService.RemoveActivity(removedActivity);

                    TextBoxActivitiesClear();
                    showPanel("Activities");
                    MessageBox.Show("Activity is removed.", "SUCCESS");
                }
                else if (dialogResult == DialogResult.No)
                {
                    return;
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        // 4th Assignment Variant B: Activities For Students Methods (Vedat's Part)

        private void DisplayActivites()
        {
            activityService = new ActivityService();
            List<Activity> activitiesList = activityService.GetActivities();

            // Clear the items
            listViewStudentActivites.Items.Clear();
            listViewStudentActivites.FullRowSelect = true;

            foreach (Activity activity in activitiesList)
            {
                ListViewItem item = new ListViewItem(activity.ActivityId.ToString());
                item.SubItems.Add(activity.ActivityName);
                item.SubItems.Add(activity.ActivityDateTime.ToString("dd/MM/yyyy H:mm"));
                item.Tag = activity;

                listViewStudentActivites.Items.Add(item);
            }
        }

        private void DisplayParticipant()
        {
            ActivityStudentService = new ActivityStudentService();
            if (listViewStudentActivites.SelectedItems.Count == 0)
            {
                return;
            }

            int activityId = Convert.ToInt32(listViewStudentActivites.SelectedItems[0].Text);

            listViewParticipators.Items.Clear();
            listViewParticipators.FullRowSelect = true;

            List<Student> activityStudents = ActivityStudentService.GetParticipants(activityId);

            foreach (Student activityStudent in activityStudents)
            {
                ListViewItem li = new ListViewItem(activityStudent.Number.ToString());
                li.SubItems.Add(activityStudent.FirstName);
                li.SubItems.Add(activityStudent.LastName);
                li.SubItems.Add(activityStudent.BirthDate.ToString("dd/MM/yyyy"));
                li.Tag = activityStudent;

                listViewParticipators.Items.Add(li);
            }

        }

        private void DisplayNonParticipant()
        {
            ActivityStudentService = new ActivityStudentService();
            if (listViewStudentActivites.SelectedItems.Count == 0)
            {
                return;
            }

            int activityId = Convert.ToInt32(listViewStudentActivites.SelectedItems[0].Text);

            listViewNotParticipators.Items.Clear();
            listViewNotParticipators.FullRowSelect = true;

            List<Student> nonParticipators = ActivityStudentService.GetNotParticipators(activityId);

            foreach (Student nonParticipator in nonParticipators)
            {
                ListViewItem li = new ListViewItem(nonParticipator.Number.ToString());
                li.SubItems.Add(nonParticipator.FirstName);
                li.SubItems.Add(nonParticipator.LastName);
                li.SubItems.Add(nonParticipator.BirthDate.ToString("dd/MM/yyyy"));
                li.Tag = nonParticipator;

                listViewNotParticipators.Items.Add(li);
            }

        }
        // Activities For Students Functions

        private void EnableButtons()
        {
            btnRemoveParticipant.Enabled = false;
            btnAddParticipant.Enabled = false;
        }

        private void btnAddParticipant_Click(object sender, EventArgs e)
        {
            if (listViewNotParticipators.SelectedItems.Count == 0 || listViewNotParticipators.SelectedItems.Count == 0)
            {
                return;
            }
            ActivityStudent activityStudent = new ActivityStudent();

            Student student = (Student)listViewNotParticipators.SelectedItems[0].Tag;
            Activity activity = (Activity)listViewStudentActivites.SelectedItems[0].Tag;

            activityStudent.StudentId = student.Number;
            activityStudent.ActivityId = activity.ActivityId;

            ActivityStudentService.AddParticipant(activityStudent);

            DisplayParticipant();
            DisplayNonParticipant();
        }

        private void btnRemoveParticipant_Click(object sender, EventArgs e)
        {
            if (listViewStudentActivites.SelectedItems.Count == 0 || listViewParticipators.SelectedItems.Count == 0)
            {
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Are you sure you wish to remove this participant?", "CONFIRMATION", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ActivityStudent activityStudent = new ActivityStudent();

                Student student = (Student)listViewParticipators.SelectedItems[0].Tag;
                Activity activity = (Activity)listViewStudentActivites.SelectedItems[0].Tag;

                activityStudent.StudentId = student.Number;
                activityStudent.ActivityId = activity.ActivityId;

                ActivityStudentService.DeleteParticipant(activityStudent);
            }
            else
            {
                return;
            }

            DisplayParticipant();
            DisplayNonParticipant();
        }

        private void listViewStudentActivites_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listViewStudentActivites.SelectedItems.Count == 0)
            {
                return;
            }

            Activity activity = (Activity)listViewStudentActivites.SelectedItems[0].Tag;
            txtActivityId.Text = activity.ActivityId.ToString();

            // Show the two listview again
            DisplayParticipant();
            DisplayNonParticipant();
        }

        private void listViewParticipators_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnRemoveParticipant.Enabled = (listViewParticipators.SelectedItems.Count >= 0);
            listViewNotParticipators.SelectedIndices.Clear();
            btnAddParticipant.Enabled = false;

            if (listViewParticipators.SelectedItems.Count == 0)
            {
                return;
            }

            Student student = (Student)listViewParticipators.SelectedItems[0].Tag;
            txtStudentId.Text = student.Number.ToString();
        }

        private void listViewNotParticipators_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddParticipant.Enabled = (listViewNotParticipators.SelectedItems.Count >= 0);
            listViewParticipators.SelectedIndices.Clear();
            btnRemoveParticipant.Enabled = false;

            if (listViewNotParticipators.SelectedItems.Count == 0)
            {
                return;
            }

            Student student = (Student)listViewNotParticipators.SelectedItems[0].Tag;
            txtStudentId.Text = student.Number.ToString();
        }
    }
}
