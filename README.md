# Systemic Tool

## INTRODUCTION

The idea of this tool came from two projects we made during the first year of
Cnam Enjmin’s master degree of video game.  
During these projects, I noticed as I collaborated with the designers that given
their **little experience** with Game engines such as Unity, they had quite a difficulty to go and **confidently change the game’s parameters**.  
They thought that they might damage the game or make a fatal error. This observation came with the finding that Unity’s **hierarchy** was not really helping.  
With enough objects and different folders, it became a **hurdle to go through** objects to change their parameters. And for a simple reason : you can only see one object’s inspector at a time.  
The fact that you need to go **back and forth** from one object to another **is tedious**. And adding to that the fact that the hierarchy can really go all over the place make it easy to get lost.

![Sans titre-2](https://github.com/user-attachments/assets/a91fe91d-899f-468d-869b-4f21e3dcfb8e)  
A scene a bit crowded

## MY SOLUTION : A TOOL
I tried to think of a tool that would make the life of designers easier.  
And the first thing I thought about was a **graph view** just like Unity’s Shader Graph, but all of the **nodes represent a game object**. And their components are viewed in the nodes with their parameters updating in real time.  
This way anyone can see all the objects and **organize them by moving them** around and grouping them as they want.

### Tags

After a bit of thinking I realized that it would be **overwhelming** to have every object of the scene represented in the graph.  
So, I went for adding a **tag system** to only get tagged objects. This tag system needs the programmer of the game using the tool to add an attribute to the component’s classes.  
The added tag gets **retrieved and loaded to the tool**’s filter.  
The designers can create a viewdata scriptable object in which they can choose the tags they want to see in the tool then open it.  
This scriptable object approach enables the player to have **different views**, and different people can have their **personal view**.

### Sub Windows  

In the tool’s window I added two different **sub windows**, one is the current data window, the other is the preview window.  
Added to that is a **button at the top**, left of the play button, made to open the tool window with the first view data found in the project folders.

![Sans titre-3](https://github.com/user-attachments/assets/f20f3642-0a72-412b-adaa-199d7451ec11)  
Open button  

The **current data window** holds the current data of the viewdata scriptable object, so that the designer can **change the tags** of the view from there and change the scriptable object viewed.  

![Sans titre-4](https://github.com/user-attachments/assets/68b0d2e9-e0be-448c-aace-bd91d040785a)  
Current data window  

The **preview window** will render a camera view rotating around the selected object so that the user can **see what he is modifying**.

![Sans titre-5](https://github.com/user-attachments/assets/975bf9b0-39bf-4419-924e-966641146562)  
preview window

### Nodes
The tool is made of multiple **nodes** that represents the objects present in the scene. These are analyzed by the tool and render the different **components in fold outs with all their fields**. The components rendered are the ones present in the tags.  

![Sans titre-6](https://github.com/user-attachments/assets/cc478cca-6a78-41e1-ac00-48cdaafe51ed)  
node with two components  

The nodes can display any type of variable:  

![Sans titre-7](https://github.com/user-attachments/assets/58f01bbc-1188-46c6-80f8-3365328f6686)  
Variety of variables in a node  

There are also **two buttons** on the top right of the nodes, “**Select**” will automatically select the node’s gameobject in the **hierarchy**, it simplifies the process of acceding them.  
“**Focus**” moves the camera to face the node’s gameobject in the scene. Just like the “**F**” shortcut.  
Each node is **saved** in the scriptable object so that their position is not reset every time.  
The main goal of the tool is to ease the designer’s experience and make it as **efficient** as possible.  This is why I wanted to be sure every interaction is quick, easy and intuitive. And its visual elements like the preview window help not having to **change windows too often**.

## TECHNICAL POINTS AND CHALLENGES

First things first I had to search online for possible **technologies** already existing that would help my work. I first tried to make a simple node system using **Unity Editor’s** editor window, I had a simple window and could create nodes with a custom class ‘Node’ that would save the position, size, state, and title of the node.  
Then it would draw a box each frame using Unity’s **GUI api**.  

Unity’s GUI Api :  
https://docs.unity3d.com/ScriptReference/GUI.html  

### Graph View 

It worked well at first, but I found out about **Unity’s Graph View** that would manage all the basics of a graph view like Zooming, view dragging, node dragging, rectangle selector and click selector.  
As there already was this system made by unity, I decided to use it so I would concentrate on the design of the tool and not its graph view only.  

I discovered the graph view with this youtube video: https://youtu.be/uXxBXGI-05k

I followed its steps to setup my Graph View before going on my own as I do not use a **flow system** (There is no logic application in my tool).  

I then discovered this Unity documentation about unity’s editor : https://www.foundations.unity.com.  
This was helpful to understand the inner workings of the editor.  

### Unity’s UI Tools  

As it turns out we can really create any kind of window in unity, and it’s not that complicated given the multiple **tools at our disposal**.  
The first and most useful is the **UI Toolkit Debugger**. As its name suggests it will help **analyze** everything we can see in a panel. It shows us the hierarchy of the panel and all its elements.  

![Sans titre-8](https://github.com/user-attachments/assets/8401b8ad-f6b8-4a5b-a612-faffaf753df1)  
UI Toolkit Debugger  

As we can see in fig.8 the debugger highlights the hovered or selected elements. It helped me understand the **organization of the editors**. In fact, a lot of the editor elements are “**Visual Element**”. And one of its base methods is Add(), this simply add another Visual Element to its content container. This, in turn create a parent/children hierarchy. Just like Unity’s gameobjects !  

Some of the UI Elements are types of **containers** like Tree View, List View, Group Box or Dropdown. Each container can contain other elements with the Add() method.  

This is how I made most of my editor UI. But there is also another **tool** provided by Unity that is especially useful to make custom inspectors : **UI Builder**.  

This tool help you create a view and see it **live while designing it**. It then gives you an UXML file that you can use in code to create an instance of it.  

![Sans titre-10](https://github.com/user-attachments/assets/f8747a90-bcab-486c-b1a3-5a2535cfe5f6)  
UI Builder   

### 5.3 USS  

There is also a **style sheet** that you can use to manage every UI’s style, **USS**. Just like Html’s **CSS**.  

These are an effective way to **manage UIs**, even in-game, as it will keep the style between all objects and update them too.  

![Sans titre-11](https://github.com/user-attachments/assets/3add0e40-b9bc-406b-8e3f-247e9092ad87)  
Example of a USS file. (Grid of the graphview)  

You can use USS to set styles of what they call “**Selectors**”. Selectors can be Types, Classes, Names (Elements with a name attribute) or Universal (any) :  

![Sans titre-12](https://github.com/user-attachments/assets/3fb05563-80e4-4081-8d61-cd3e3151e364)  
Selectors  

These are filled with **rules** to be applied.  

## CODE STRUCTURE  

For the code part I made **two parts**, one is the “**SystemicToolWindow**” that manages window and **data side** of the nodes.  
The other part is the “**SystemicGraphView**” class which manages the **graphic** and representation side of nodes.  
Lastly there is the **nodes** themselves which have a **data class** to be saved in a scriptable object, and a **VisualElement class** to oversee the graphic part.  
I will go through these classes and how they work.  

### Systemic Tool Window 

The systemic tool window class manages or initialize the **opening and closing** of the window, the **buttons and toolbar**, the preview camera, the **prop windows**, the **nodes retrieval** in the Unity Scene and **tags retrieval** in the different Monobehavior classes.  

![Sans titre-13](https://github.com/user-attachments/assets/ef5262bc-67ee-4c80-b7be-c8e084b55fd2)  
The SystemicToolWindow Class  

The buttons are simply the adding of the open button to the tool bar using the Tool **Bar Extender** made by **Marijnz**.  

And the adding of the open button from the Menu Items : 

![Sans titre-14](https://github.com/user-attachments/assets/446bee75-88b2-4967-a582-b61e7d5a143f)  

The **opening** of the editor window will call the `CreateWindow<SystemicToolWindow>()` method and load the wanted **Systemic View Data** (which holds the tags mask and nodes saved).

![Sans titre-15](https://github.com/user-attachments/assets/a858cfb4-88a7-48cd-a184-b40b177ea067)  

The closing simply calls the “window.Close()” method and destroy it.  
The preview camera part only **activates/deactivates** the preview window and set the focused object.  

![Sans titre-16](https://github.com/user-attachments/assets/5618f9ca-6a75-4b4a-9f09-a87a82933961)  
Preview activation  

The “m_currentPreview” object then render a **camera into a Render Texture** that is then applied to the preview window.  
The movement of the camera was made using the **EditorApplication.update** event that is the equivalent of the Monobehavior’s Update. Except that it updates only on user interaction (mouse selection for example), so I had to force the editor window to call the **Repaint()** (which forces the call of the update) in the update function to have a constant movement.  

![Sans titre-17](https://github.com/user-attachments/assets/5065a395-46f1-483e-ace7-5c4e0c5728bc)  

The prop window creates the current data window (top left) and the preview window. 

![Sans titre-18](https://github.com/user-attachments/assets/e3e0f733-c1b9-4d6d-aef2-31d24c032e8f)  

We then have the **nodes data management**; it retrieves the components and make them a node if they contain a **Systemic Tag Attribute** and are their tags contained in the **mask**.  
It checks the integrity of existing nodes too, should we keep them.  

![Sans titre-19](https://github.com/user-attachments/assets/fc155116-de07-4bfa-acb7-b5801ff60d90)  

Note that we don’t delete them if they’re not in the mask, we just **hide** them. It’s made so that if we change the mask parameter it won’t reset their position.  
Has seen in the last piece of code, we have the Tag management too :  

![Sans titre-20](https://github.com/user-attachments/assets/afbff6b6-3e97-4bf8-9a90-a4a773cc0dfb)  

![Sans titre-21](https://github.com/user-attachments/assets/7557792a-7c72-411e-b793-b937ec706830)  

### Systemic Graph View 

The Systemic Graph View manages the **visual part of the window**, it holds a dictionary of all node visuals present and its setups all the manipulators/style.  

![image](https://github.com/user-attachments/assets/a5957fdb-0588-432d-9ec3-9ddd1c724854)  

The node region oversees the adding of **new nodes to the graph view** : 

![image](https://github.com/user-attachments/assets/c4f57326-a784-4acb-b7e3-2c1394f204ac)  

### Nodes 

There are **two node classes**, one is made to **hold information** about position, title, show state and which gameobjects is linked. It will also get if the game object has a systemic attribute. 

![image](https://github.com/user-attachments/assets/df8164b5-540f-4b29-9af6-3378cdbcc56d)  
Systemic Node Data class  


They are stocked in the **Systemic View Data scriptable object** to be loaded when the view is reloaded.  

![image](https://github.com/user-attachments/assets/74b57a51-6119-4a51-8265-4e74dffc0067)  
Systemic View Data  

The other one is made to manage the **visual part of the nodes** with all the components and buttons.  

![image](https://github.com/user-attachments/assets/b4cea33c-c8fc-4ccc-8065-86f276580d6d)  
Systemic editor node (visual part)  

Here, we can see the visual setup with the **buttons**, and their call back (Select and Focus) and the **node’s components**. Here we use a different class to handle the node’s component :  

![image](https://github.com/user-attachments/assets/680a2008-9ff1-4314-8ffc-c923587b7e87)  
Node component  

The node component uses the **Property Field** of Unity’s UI Elements. This field **adapts to the type** of field it is bound to. After creating it we Add() it to the Content Container of the foldout object.  

Next in the Systemic Editor Node is this :  

![image](https://github.com/user-attachments/assets/6d22ac8a-56f1-4bc5-b0b7-9b3deb34fb74)  

The mouse interaction part is to set the Preview camera focus object.  

### Systemic Attribute  

At last, I wanted to talk about the **attributes** as I learned to use them during this experiment.

![image](https://github.com/user-attachments/assets/b104683d-ff16-43c7-b081-9413b8528f2a)  

This class is an attribute, its **name** is what we must write in [] to add it to a class. As you can see it uses an attribute itself, to **specify which type** of elements it is appliable to. The Attribute can **hold information** as you can see with the “TagName” string field.  
We can then check if a type has a custom attribute using “GetCustomAttributes()”.  

![image](https://github.com/user-attachments/assets/6330d76b-8cb4-4554-8552-51112b703de5)  
Example of custom attribute check
