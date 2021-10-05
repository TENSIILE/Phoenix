# Phoenix

![](https://sun9-72.userapi.com/impg/-S10C5cQUiqodcca6ydQjVrJqNtKOaxr18a9XQ/K_KxtkdliQo.jpg?size=2560x1440&quality=96&sign=902243a3cc559906ca6a88a6e6ac94c7&type=album "Logo framework")

Phoenix is a reactive framework for building native desktop applications. Unlike other frameworks, Phoenix can be partially integrated into your project and use what you need, or completely switch to its architecture.
 
## :pushpin: Similarity
 Phoenix is very much like Facebook's **React** library. On this, the calculation was made that there was no need to come up with something original, to which people would get used, but to take the technology they liked from another language and implement it in another language.
 
## Installation
At this stage of the development of the framework, it is not in the NuGet package manager. Therefore, you will have to:

 1. Go to bin folder and then release.
 2. Find the phoenix.dll file and download it to your computer.
 3. Open your project in Visual Studio.
 4. In Solution Explorer, expand the References tab.
 5. And add a new dependency to the project.
 

## Features
- Hooks;
- Creation of your own hooks;
- Various types of storages;
- Data binding;
- Single-page and multi-page applications;
- Reactive render;
- Instant transmission of data through forms;
- Easy management of all existing forms through the container.
- Additional features;
- Etc.

## Example code

I would like to immediately demonstrate what the code written in Phoenix looks like. As an example, I took a primitive task of incrementing and decrementing a number and outputting it to a label.

```Csharp

using System;
using Phoenix;
using Phoenix.Core;

namespace WindowsFormsApp
{
    internal struct COMMANDS
    {
        public const string INCREMENT = "INCREMENT";
        public const string DECREMENT = "DECREMENT";
    }

    public partial class MainForm: PhoenixForm
    {
        private Reducer<int> _reducer;
        
        public MainForm()
        {
            InitializeComponent();
            Init();

            _reducer = UseReducer(Reducer, 3);
        }

        private int Reducer(State<int> state, ReducerAction action)
        {
            switch (action.Type)
            { 
                case COMMANDS.DECREMENT:
                    return state.Value - 1;
                case COMMANDS.INCREMENT:
                    return state.Value + 1;
                default:
                    return state.Value;
            }
        }
        
        private void PhoenixButton1_Click(object sender, EventArgs e)
        {
            _reducer.Dispatch(COMMANDS.INCREMENT);
        }

        private void PhoenixButton2_Click(object sender, EventArgs e)
        {
            _reducer.Dispatch(COMMANDS.DECREMENT);
        }

        protected override void Render()
        {
            label1.Text = _reducer.Value.ToString();
        }
    }
}
```
