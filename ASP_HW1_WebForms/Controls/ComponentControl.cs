using System;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Homework2;
using System.Drawing;

namespace ASP_HW1_WebForms
{
    public class ComponentControl : Panel
    {
        //private int id; // Ключ фигуры в коллекции фигур
        private string name; // имя компонента в коллекции - ключ

        private IDictionary<string, Component> componentList; // Коллекция устройств

        private Label infoLabel; // Ярлык для отображения информации

        private Button onButton; // Кнопка для включения
        private Button offButton; // Кнопка для выключения
        private Button openButton; // Кнопка для открытия
        private Button closeButton; // Кнопка для закрытия
        private Button nextButton; // Кнопка для следующего канала
        private Button prevButton; // Кнопка для предыдущего канала

        private TextBox temperBox; // Поле ввода для отображения/установки значения температуры
        private TextBox volumeBox; // Поле ввода для отображения/установки значения громкости
        private DropDownList modesList; // Выпадающий список для выбора режима

        private Button setButton; // Кнопка для установки значения температуры, канала, выбранного режима
        private Button deleteButton; // Кнопка для удаления из коллекции


        //конструктор
        public ComponentControl(string name, IDictionary<string, Component> componentList)
        {
            this.name = name;
            this.componentList = componentList;

            Initializer();
        }

        // Инициализатор графики
        protected void Initializer()
        {
            CssClass = "component-div";

            //Controls.Add(Span("Компонент: " + componentList[name].Name + "<br />"));
            infoLabel = new Label();
            infoLabel.Text = componentList[name].Info();
            infoLabel.ID = "i" + name.ToString();
            Controls.Add(infoLabel);
            Controls.Add(Span("<br />"));

            if (componentList[name] is IPowerable)
            {
                onButton = new Button();
                onButton.Text = "Вкл";
                onButton.Click += onButton_Click;
                onButton.ID = "on" + name.ToString();
                onButton.BackColor = Color.LightGreen;
                Controls.Add(onButton);

                offButton = new Button();
                offButton.Text = "Погасить";
                offButton.Click += offButton_Click;
                offButton.ID = "off" + name.ToString();
                offButton.BackColor = Color.LightPink;
                Controls.Add(offButton);
            }

            //Controls.Add(Span("<br />"));

            if (componentList[name] is IOpenable)
            {
                openButton = new Button();
                openButton.Text = "Открыть";
                openButton.Click += openButton_Click;
                openButton.ID = "open" + name.ToString();
                Controls.Add(openButton);

                closeButton = new Button();
                closeButton.Text = "Закрыть";
                closeButton.Click += closeButton_Click;
                closeButton.ID = "close" + name.ToString();
                Controls.Add(closeButton);
            }

            if (componentList[name] is TV)
            {
                nextButton = new Button();
                nextButton.Text = "След.канал";
                nextButton.Click+=nextButton_Click;
                nextButton.ID = "next" + name.ToString();
                Controls.Add(nextButton);

                prevButton = new Button();
                prevButton.Text = "Пред.канал";
                prevButton.Click += prevButton_Click;
                prevButton.ID = "prev" + name.ToString();
                Controls.Add(prevButton);
            }

            //Controls.Add(Span("<br />"));

            if (componentList[name] is MediaCenter)
            {
                Controls.Add(Span("Громкость: "));
                int volume = ((MediaCenter)componentList[name]).Volume;
                volumeBox = TextBox(volume);
                volumeBox.ID = "v" + name.ToString();
                Controls.Add(volumeBox);

                setButton = new Button();
                setButton.Text = "Установить";
                setButton.Click += SetButtonClick;
                setButton.ID = "set" + name.ToString();
                Controls.Add(setButton);
            }

            if (componentList[name] is Oven)
            {
                Controls.Add(Span("Температура: "));
                int temper = ((Oven)componentList[name]).Temperature;
                temperBox = TextBox(temper);
                temperBox.ID = "t" + name.ToString();
                Controls.Add(temperBox);

                setButton = new Button();
                setButton.Text = "Установить";
                setButton.Click += SetButtonClick;
                setButton.ID = "set" + name.ToString();
                Controls.Add(setButton);
            }

            if (componentList[name] is Fridge)
            {
                Controls.Add(Span("Режим: "));
                FridgeModes mode = ((Fridge)componentList[name]).Mode;
                modesList = DropDownList(mode);
                modesList.ID = "m" + name.ToString();
                Controls.Add(modesList);


                setButton = new Button();
                setButton.Text = "Установить";
                setButton.Click += SetButtonClick;
                setButton.ID = "set" + name.ToString();
                Controls.Add(setButton);
            }



            Controls.Add(Span("<br />"));

            deleteButton = new Button();
            deleteButton.Text = "Удалить";
            deleteButton.Click += DeleteButtonClick;
            deleteButton.ID = "d" + name.ToString();
            Controls.Add(deleteButton);

            Controls.Add(Span("<br />"));

            //infoLabel = new Label();
            //infoLabel.Text = componentList[name].Info();
            //infoLabel.ID = "i" + name.ToString();
            //Controls.Add(infoLabel);
            //Controls.Add(Span("<br />"));
        }


        //рисуем html
        protected HtmlGenericControl Span(string innerHTML)
        {
            HtmlGenericControl span = new HtmlGenericControl("span");
            span.InnerHtml = innerHTML;
            return span;
        }

        //рисуем текст-бокс
        protected TextBox TextBox(int value)
        {
            TextBox textBox = new TextBox();
            textBox.Columns = 3;
            textBox.Text = value.ToString();
            return textBox;
        }

        ////рисуем лейбл
        //protected Label Label(double value)
        //{
        //    Label label = new Label();
        //    label.Text = value.ToString();
        //    return label;
        //}

        //рисуем выпадающий список
        protected DropDownList DropDownList(FridgeModes value)
        {
            DropDownList dropDownList = new DropDownList();
            dropDownList.Text = value.ToString();
            //dropDownList.Items.Add(FridgeModes.normal.ToString());
            //dropDownList.Items.Add(FridgeModes.north.ToString());
            //dropDownList.Items.Add(FridgeModes.south.ToString());
            dropDownList.Items.Add("нормальный");
            dropDownList.Items.Add("северный");
            dropDownList.Items.Add("южный");

            return dropDownList;
        }


        // обработчики кнопок включения, выключения, открытия, закрытия, переключения каналов
        // в каждой перерисовываем Info()
        void onButton_Click(object sender, EventArgs e)
        {
            ((IPowerable)componentList[name]).PowerOn();
            infoLabel.Text = componentList[name].Info();
        }

        void offButton_Click(object sender, EventArgs e)
        {
            ((IPowerable)componentList[name]).PowerOff();
            infoLabel.Text = componentList[name].Info();
        }

        void openButton_Click(object sender, EventArgs e)
        {
            ((IOpenable)componentList[name]).Open();
            infoLabel.Text = componentList[name].Info();
        }

        void closeButton_Click(object sender, EventArgs e)
        {
            ((IOpenable)componentList[name]).Close();
            infoLabel.Text = componentList[name].Info();
        }

        void prevButton_Click(object sender, EventArgs e)
        {
            ((TV)componentList[name]).PrevChannel();
            infoLabel.Text = componentList[name].Info();
        }

        void nextButton_Click(object sender, EventArgs e)
        {
            ((TV)componentList[name]).NextChannel();
            infoLabel.Text = componentList[name].Info();
        }

        // Обработчик нажатия кнопки для установки значения
        protected void SetButtonClick(object sender, EventArgs e)
        {
            if (componentList[name] is MediaCenter)
            {
                int v = Convert.ToInt32(volumeBox.Text);
                ((MediaCenter)componentList[name]).Volume = v;
                infoLabel.Text = componentList[name].Info();
            }

            if (componentList[name] is Oven)
            {
                int t = Convert.ToInt32(temperBox.Text);
                ((Oven)componentList[name]).SetTemper(t);
                infoLabel.Text = componentList[name].Info();
            }

            if (componentList[name] is Fridge)
            {
                switch (modesList.SelectedValue)
                {   
                    case ("нормальный"):
                        ((Fridge)componentList[name]).Normal();
                        break;
                    case ("северный"):
                        ((Fridge)componentList[name]).North();
                        break;
                    case ("южный"):
                        ((Fridge)componentList[name]).South();
                        break;
                    default:
                        break;
                }

                infoLabel.Text = componentList[name].Info();
                
                //string selectedMode = modesList.SelectedValue;
                //infoLabel.Text += selectedMode;
                //FridgeModes m = 
                //((Fridge)componentList[name]).Mode = m;
            }
        }


        // Обработчик нажатия кнопки для удаления из коллекции
        protected void DeleteButtonClick(object sender, EventArgs e)
        {
            componentList.Remove(name); // Удаление из коллекции
            Parent.Controls.Remove(this); // Удаление графики
        }
    }
}