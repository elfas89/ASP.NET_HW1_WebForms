using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Homework2;

namespace ASP_HW1_WebForms
{
    public partial class Default : System.Web.UI.Page
    {
        // Коллекция
        //private IDictionary<int, Figure> figuresDictionary;
        private IDictionary<string, Component> componentList;
            //= new Dictionary<string, Component>();


        protected void Page_Init(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                //не первое обращение, берем коллекцию из сессии
                componentList = (SortedDictionary<string, Component>)Session["Components"];
            }
            else
            {
                //первое обращение, создаем коллекцию, наполняем по умолчанию
                componentList = new SortedDictionary<string, Component>();
                componentList.Add("Sony", new TV("Sony"));
                componentList.Add("Aiwa", new MediaCenter("Aiwa", 88.8));
                componentList.Add("LG", new Fridge("LG"));
                
                //figuresDictionary.Add(3, new Circle("Круг", 1));
                //figuresDictionary.Add(4, new Sphere("Сфера", 1));



                // кладем коллекцию в сессию
                Session["Components"] = componentList;
                //Session["NextId"] = 5;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack)
            {
                addComponentButton.Click += AddComponentButtonClick;
            }
            InitialiseComponentPanel();
        }

        // Создание элементов графики для всех компонентов в коллекции
        protected void InitialiseComponentPanel()
        {
            foreach (string key in componentList.Keys)
            {
                сomponentPanel.Controls.Add(new ComponentControl(key, componentList));
            }
        }

        // Обработчик нажатия кнопки добавления компонентов
        protected void AddComponentButtonClick(object sender, EventArgs e)
        {
            Component newComponent;
            switch (dropDownComponentList.SelectedIndex)
            {
                default:
                    newComponent = new TV(nameComponentBox.Text);
                    break;
                case 1:
                    newComponent = new Fridge(nameComponentBox.Text);
                    break;
                case 2:
                    newComponent = new Stove(nameComponentBox.Text);
                    break;
                case 3:
                    newComponent = new Oven(nameComponentBox.Text, 0, 96);
                    break;
                case 4:
                    newComponent = new MediaCenter(nameComponentBox.Text, 88.8);
                    break;

            }

            //int id = (int)Session["NextId"];
            //figuresDictionary.Add(id, newFigure); // Добавление фигуры в коллекцию
            //figuresPanel.Controls.Add(new FigureControl(id, figuresDictionary)); // Добавление графики для фигуры
            //id++;
            //Session["NextId"] = id;

            // проверка заполненности
            //проверка наличия имени
            string name = nameComponentBox.Text;
            componentList.Add(name, newComponent);
            сomponentPanel.Controls.Add(new ComponentControl(name, componentList));
            //Server.Transfer();

        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            Session["Components"] = componentList;
        }
    }
}