﻿@using gameOfLife.Models
@model LoginViewModel
@{
    ViewBag.Title = "Log in";
}


<h1>Game of Life</h1>
<canvas id = "myCanvas" width="160" height="160" style="border:1px solid #d3d3d3;"></canvas>
<script>
        // Basic Shape Contructor to store Shapes position and properties

        function Shape(x, y, r, c, alive) {
            this.x = x;
            this.y = y;
            this.w = 15;
            this.h = 15;
            this.r = r;
            this.c = c;
            this.alive = alive;
            if (alive == 0)
                this.fill = "grey";
            if (alive == 1)
                this.fill = "green";
        }

        //Stores canvas
        var elem = document.getElementById('myCanvas');

var gridArray = createArray();
glider();
draw();

function createArray()
{
    var arr = new Array(12);
    for (var i = 0; i < arr.length; i++)
    { //creates 2D array
        arr[i] = new Array(12);
    }

    //Initialize Array
    var xPos = 0;
    var yPos = 0;
    for (var r = 0; r < 12; r++)
    {
        for (var c = 0; c < 12; c++)
        {
            arr[r][c] = new Shape(xPos, yPos, r, c, 0);
            if (c == 0 || c == 11)  //skip if edge
                continue;
            xPos += 16;
        }
        xPos = 0;

        if (r == 0 || r == 11)  //skip if edge
            continue;
        yPos += 16;
    }

    //Store info about canvas
    elemLeft = elem.offsetLeft;
    elemTop = elem.offsetTop;
    context = elem.getContext('2d');

    //Add Hit test event listener
    elem.addEventListener('click', function(event) {
        var x = event.pageX - elemLeft,
                    y = event.pageY - elemTop;
        console.log(x, y);

        //run through array and check if mouse click matched an elements location
        for (var row = 1; row < 11; row++)
        {
            for (var col = 1; col < 11; col++)
            {
                var element = arr[row][col];
                if (y > element.y && y < element.y + element.h && x > element.x && x < element.x + element.w)
                {
                    bringAlive(element);
                    draw();
                }
            }
        }

    }, false);

    return arr;

}//end createArray

//Initializes neighborhood to glider formation
function glider()
{
    bringAlive(gridArray[6][4]);
    bringAlive(gridArray[6][5]);
    bringAlive(gridArray[6][6]);
    bringAlive(gridArray[5][6]);
    bringAlive(gridArray[4][5]);
    draw();

}

//Turns on specified element
function bringAlive(element)
{
    gridArray[element.r][element.c] = new Shape(element.x, element.y, element.r, element.c, 1);

}




//counts number of neighbors that are alive

@functions{



           public class Shape
{
    int x;
    int y;
    int w;
    int h;
    int r;
    int c;
    int alive;
    string fill;
    public Shape[,] gridArray;



    public Shape(int x, int y, int r, int c, int alive)
    {


        this.x = x;
        this.y = y;
        this.w = 15;
        this.h = 15;
        this.r = r;
        this.c = c;
        this.alive = alive;
        if (alive == 0)
            this.fill = "grey";
        if (alive == 1)
            this.fill = "green";
    }


    public int checkNeighbors(int x, int y)
    {
        int count = 0;

        // bottom left.
        if (IsAlive(x - 1, y - 1))
        {
            count++;
        }

        // left
        if (x != 0)
            if ((IsAlive(x - 1, y)))
            {
                count++;
            }

        //top left
        if (x != 0 && y != 0)
            if (IsAlive(x - 1, y - 1))
            {
                count++;
            }

        // Check cell on the top.
        if (y != 0)
            if (IsAlive(x, y - 1))
            {
                count++;
            }

        // right
        if (x != gridArray.Length - 1)
            if (IsAlive(x + 1, y))
            {
                count++;
            }

        //bottom right
        if (x != gridArray.Length - 1 && y != gridArray.Length - 1)
            if (IsAlive(x + 1, y + 1))
            {
                count++;
            }

        // bottom
        if (y != gridArray.Length - 1)
            if (IsAlive(x, y + 1))
            {
                count++;
            }

        //top right
        if (x != gridArray.Length - 1 && y != 0)
            if (IsAlive(x + 1, y - 1))
            {
                count++;
            }

        return count;
    }

    public Shape[,] CreateArray()
    {
        Shape[,] arr = new Shape[12, 12];
        int xPos = 0;
        int yPos = 0;

        for (int r = 0; r < 12; r++)
        {
            for (int c = 0; c < 12; c++)
            {
                arr[r, c] = new Shape(xPos, yPos, r, c, 0);
                if (c == 0 || c == 11)  //skip if edge
                    continue;
                xPos += 16;
            }
            xPos = 0;

            if (r == 0 || r == 11)  //skip if edge
                continue;
            yPos += 16;
        }
        return arr;
    }

    public void Update()
    {
        Shape[,] newArray = gridArray;

        for (int gridRow = 1; gridRow < 11; gridRow++)
        {
            for (int gridCol = 1; gridCol < 11; gridCol++)
            {
                int count = checkNeighbors(gridRow, gridCol);

                if (count == 3)
                {
                    newArray[gridRow, gridCol].alive = 1;
                }

                if (count > 3 || count < 2)
                {
                    newArray[gridRow, gridCol].alive = 0;
                }
            }
        }

        newArray = gridArray;
    }

    public bool IsAlive(int x, int y)
    {
        if (gridArray[x, y].alive == 1)
        {
            return true;

        }
        else
        {
            return false;
        }
    }
}
           }
           
    
               draw();

//turns on neighbor in newArray
function aliveNeighbor(element)
{
    newArray[element.r][element.c] = new Shape(element.x, element.y, element.r, element.c, 1);

}

//turns off neighbor in newArray
function killNeighbor(element)
{
    newArray[element.r][element.c] = new Shape(element.x, element.y, element.r, element.c, 0);
}
           }


           //Draws Canvas and all the elements in the array
           function draw()
{
    context = elem.getContext('2d');
    for (var i = 1; i < 11; i++)
    {
        for (var j = 1; j < 11; j++)
        {
            oRec = gridArray[i][j];
            context.fillStyle = oRec.fill;
            context.fillRect(oRec.x, oRec.y, oRec.w, oRec.h);
        }

    }
}


//Starts and stops animation on button press
var interval;
function start()
{
    interval = setInterval(update, 700);
}
function stop()
{
    clearInterval(interval);
}

        function checkNeighbors(r, c) {
            var amount = 0;

            function isAlive(r, c) {
                if (gridArray[r][c].alive == 1)
                    return true;
                else
                    return false;
            }

            //starting in upper left corner, checking clock-wise
            if (isAlive(r - 1, c - 1)) amount++;
            if (isAlive(r - 1, c)) amount++;
            if (isAlive(r - 1, c + 1)) amount++;
            if (isAlive(r, c + 1)) amount++;
            if (isAlive(r + 1, c + 1)) amount++;
            if (isAlive(r + 1, c)) amount++;
            if (isAlive(r + 1, c - 1)) amount++;
            if (isAlive(r, c - 1)) amount++;

            return amount;
        }

        //steps through game, updating new neighborhood
        function update() {
            //Create new Temporary array to store new game state
            var newArray = [];
            for (var i = 0; i < gridArray.length; i++)
                newArray[i] = gridArray[i].slice();

            for (var gridRow = 1; gridRow < 11; gridRow++) {
                for (var gridCol = 1; gridCol < 11; gridCol++) {
                    var count = checkNeighbors(gridRow, gridCol);

                    //turn on if cell has 3 neighbors and is dead
                    if (count == 3)
                        aliveNeighbor(newArray[gridRow][gridCol]);

                    //turn off if cell has less than 2 or more than 3 and is alive
                    if (count > 3 || count < 2)
                        killNeighbor(newArray[gridRow][gridCol]);

                }
            }

            //copy newArray into previous array and redraw
            gridArray = [];
            for (var i = 0; i < newArray.length; i++)
                gridArray[i] = newArray[i].slice();
            draw();

            //turns on neighbor in newArray
            function aliveNeighbor(element) {
                newArray[element.r][element.c] = new Shape(element.x, element.y, element.r, element.c, 1);

            }

            //turns off neighbor in newArray
            function killNeighbor(element) {
                newArray[element.r][element.c] = new Shape(element.x, element.y, element.r, element.c, 0);
            }
        }


        //Draws Canvas and all the elements in the array
        function draw() {
            context = elem.getContext('2d');
            for (var i = 1; i < 11; i++) {
                for (var j = 1; j < 11; j++) {
                    oRec = gridArray[i][j];
                    context.fillStyle = oRec.fill;
                    context.fillRect(oRec.x, oRec.y, oRec.w, oRec.h);
                }

            }
        }


        //Starts and stops animation on button press
        var interval;
        function start() {
            interval = setInterval(update, 700);
        }
        function stop() {
            clearInterval(interval);
        }


</script>

<p>Yay!!!! It's ugly and in need of some major cleaning and none of it is in .NET so we'll need to figure out how to convert it over, along with the thousands of other things that still need to be done.But it works.It's something.</p>
<button onclick = "start()" > Start </ button >
< button onclick="update()"> Next</button>
<button onclick = "stop()" > Stop </ button >

@section Scripts {
    @Scripts.Render("~/bundles/jqueryval")
}