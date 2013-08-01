using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace BabyApp
{
    public partial class PivotPage1 : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public PivotPage1()
        {
            InitializeComponent();

            Animals = new ObservableCollection<BoxGrid>();

            LoadPicsIntoCollection();

        }

        #region "Properties"

        private void NotifyPropertyChanged(String propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private ObservableCollection<BoxGrid> _animals;
        public ObservableCollection<BoxGrid> Animals
        {
            get { return _animals; }
            set { _animals = value; NotifyPropertyChanged("Animals"); }
        }

        private BoxGrid _animal;
        public BoxGrid Animal
        {
            get { return _animal; }
            set { _animal = value; NotifyPropertyChanged("Animal"); }
        }

        #endregion "Properties"

        #region "Events"

        private void Box_Click(object sender, EventArgs e)
        {
            string tag = ((Button)sender).Tag.ToString();           
        }

        #endregion "Events"

        #region "Methods"

        private void LoadPicsIntoCollection()
        {
            try
            {
                Animal = new BoxGrid();
                Animal.Picture1 = new Box("Anteater", "/Assets/Pics/Animals/anteater80x100.png", "/Assets/Pics/Animals/anteater480x800.png", "/Assets/Sounds/Animals/anteater.wav");
                Animal.Picture2 = new Box("Antelope", "/Assets/Pics/Animals/antelope80x100.png", "/Assets/Pics/Animals/antelope480x800.png", "/Assets/Sounds/Animals/antelope.wav");
                Animal.Picture3 = new Box("Badger", "/Assets/Pics/Animals/badger80x100.png", "/Assets/Pics/Animals/badger480x800.png", "/Assets/Sounds/Animals/badger.wav");
                Animal.Picture4 = new Box("Bear", "/Assets/Pics/Animals/bear80x100.png", "/Assets/Pics/Animals/bear80x800.png", "/Assets/Sounds/Animals/bear.wav");
                Animal.Picture5 = new Box("Butterfly", "/Assets/Pics/Animals/butterfly80x100.png", "/Assets/Pics/Animals/butterfly480x800.png", "/Assets/Sounds/Animals/butterfly.wav");
                Animal.Picture6 = new Box("Capuchinmonkey", "/Assets/Pics/Animals/capuchinmonkey80x100.png", "/Assets/Pics/Animals/capuchinmonkey480x800.png", "/Assets/Sounds/Animals/capuchinmonkey.wav");
                Animal.Picture7 = new Box("Caribou", "/Assets/Pics/Animals/caribou80x100.png", "/Assets/Pics/Animals/caribou480x800.png", "/Assets/Sounds/Animals/caribou.wav");
                Animal.Picture8 = new Box("Cheetah", "/Assets/Pics/Animals/cheetah80x100.png", "/Assets/Pics/Animals/cheetah80x800.png", "/Assets/Sounds/Animals/cheetah.wav");
                Animal.Picture9 = new Box("Crab", "/Assets/Pics/Animals/crab80x100.png", "/Assets/Pics/Animals/crab480x800.png", "/Assets/Sounds/Animals/crab.wav");

                Animals.Add(Animal);

                Animal = new BoxGrid();

                Animal.Picture1 = new Box("Dog", "/Assets/Pics/Animals/dog80x100.png", "/Assets/Pics/Animals/dog480x800.png", "/Assets/Sounds/Animals/dog.wav");
                Animal.Picture2 = new Box("Dolphin", "/Assets/Pics/Animals/dolphin80x100.png", "/Assets/Pics/Animals/dolphin480x800.png", "/Assets/Sounds/Animals/dolphin.wav");
                Animal.Picture3 = new Box("Duck", "/Assets/Pics/Animals/duck80x100.png", "/Assets/Pics/Animals/duck80x800.png", "/Assets/Sounds/Animals/duck.wav");
                Animal.Picture4 = new Box("Eagle", "/Assets/Pics/Animals/eagle80x100.png", "/Assets/Pics/Animals/eagle480x800.png", "/Assets/Sounds/Animals/eagle.wav");
                Animal.Picture5 = new Box("Elephant", "/Assets/Pics/Animals/elephant80x100.png", "/Assets/Pics/Animals/elephant480x800.png", "/Assets/Sounds/Animals/elephant.wav");
                Animal.Picture6 = new Box("Fish", "/Assets/Pics/Animals/fish80x100.png", "/Assets/Pics/Animals/fish480x800.png", "/Assets/Sounds/Animals/fish.wav");
                Animal.Picture7 = new Box("Fox", "/Assets/Pics/Animals/fox80x100.png", "/Assets/Pics/Animals/fox80x800.png", "/Assets/Sounds/Animals/fox.wav");
                Animal.Picture8 = new Box("Giraffe", "/Assets/Pics/Animals/giraffe80x100.png", "/Assets/Pics/Animals/giraffe480x800.png", "/Assets/Sounds/Animals/giraffe.wav");
                Animal.Picture9 = new Box("Horse", "/Assets/Pics/Animals/horse80x100.png", "/Assets/Pics/Animals/horse480x800.png", "/Assets/Sounds/Animals/horse.wav");

                Animals.Add(Animal);

                Animal = new BoxGrid();

                Animal.Picture1 = new Box("Kitten", "/Assets/Pics/Animals/kitten80x100.png", "/Assets/Pics/Animals/kitten480x800.png", "/Assets/Sounds/Animals/kitten.wav");
                Animal.Picture2 = new Box("Lion", "/Assets/Pics/Animals/lion80x100.png", "/Assets/Pics/Animals/lion80x800.png", "/Assets/Sounds/Animals/lion.wav");
                Animal.Picture3 = new Box("Macaw", "/Assets/Pics/Animals/macaw80x100.png", "/Assets/Pics/Animals/macaw480x800.png", "/Assets/Sounds/Animals/macaw.wav");
                Animal.Picture4 = new Box("Monkey", "/Assets/Pics/Animals/monkey80x100.png", "/Assets/Pics/Animals/monkey480x800.png", "/Assets/Sounds/Animals/monkey.wav");
                Animal.Picture5 = new Box("Otter", "/Assets/Pics/Animals/otter80x100.png", "/Assets/Pics/Animals/otter480x800.png", "/Assets/Sounds/Animals/otter.wav");
                Animal.Picture6 = new Box("Owl", "/Assets/Pics/Animals/owl80x100.png", "/Assets/Pics/Animals/owl80x800.png", "/Assets/Sounds/Animals/owl.wav");
                Animal.Picture7 = new Box("Panda", "/Assets/Pics/Animals/panda80x100.png", "/Assets/Pics/Animals/panda480x800.png", "/Assets/Sounds/Animals/panda.wav");
                Animal.Picture8 = new Box("Penquin", "/Assets/Pics/Animals/penquin80x100.png", "/Assets/Pics/Animals/penquin480x800.png", "/Assets/Sounds/Animals/penquin.wav");
                Animal.Picture9 = new Box("Pig", "/Assets/Pics/Animals/pig80x100.png", "/Assets/Pics/Animals/pig80x800.png", "/Assets/Sounds/Animals/pig.wav");

                Animals.Add(Animal);

                Animal = new BoxGrid();

                Animal.Picture1 = new Box("Rabbit", "/Assets/Pics/Animals/rabbit80x100.png", "/Assets/Pics/Animals/rabbit480x800.png", "/Assets/Sounds/Animals/rabbit.wav");
                Animal.Picture2 = new Box("Shark", "/Assets/Pics/Animals/shark80x100.png", "/Assets/Pics/Animals/shark480x800.png", "/Assets/Sounds/Animals/shark.wav");
                Animal.Picture3 = new Box("Snake", "/Assets/Pics/Animals/snake80x100.png", "/Assets/Pics/Animals/snake480x800.png", "/Assets/Sounds/Animals/snake.wav");
                Animal.Picture4 = new Box("Snowleopard", "/Assets/Pics/Animals/snowleopard80x100.png", "/Assets/Pics/Animals/snowleopard480x800.png", "/Assets/Sounds/Animals/snowleopard.wav");
                Animal.Picture5 = new Box("Starfish", "/Assets/Pics/Animals/starfish80x100.png", "/Assets/Pics/Animals/starfish80x800.png", "/Assets/Sounds/Animals/starfish.wav");
                Animal.Picture6 = new Box("Sumatrantiger", "/Assets/Pics/Animals/sumatrantiger80x100.png", "/Assets/Pics/Animals/sumatrantiger480x800.png", "/Assets/Sounds/Animals/sumatrantiger.wav");
                Animal.Picture7 = new Box("Tiger", "/Assets/Pics/Animals/tiger80x100.png", "/Assets/Pics/Animals/tiger480x800.png", "/Assets/Sounds/Animals/tiger.wav");
                Animal.Picture8 = new Box("Wolf", "/Assets/Pics/Animals/wolf80x100.png", "/Assets/Pics/Animals/wolf480x800.png", "/Assets/Sounds/Animals/wolf.wav");

                Animals.Add(Animal);
            }
            catch (Exception ex)
            {

                
            }
        }


        #endregion "Methods"


    }
}