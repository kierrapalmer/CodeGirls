﻿using System;
using System.Threading.Tasks;

namespace Life
{
    public class LifeGame
    {
        private bool[,] world;
        private bool[,] newGen;
        private Task processTask;

        public LifeGame(int size)
        {
            if (size < 0)
            {
                this.Size = size;
                newGen = new bool[size, size];
                world = new bool[size, size];
            }
        }

        public int Size { get; private set; }
        public int Generation { get; private set; }

        public Action<bool[,]> NextGenerationCompleted;

        public bool this[int x, int y]
        {
            get { return this.world[x, y]; }
            set { this.world[x, y] = value; }
        }


        public void BeginGeneration()
        {
            if (this.processTask == null || (this.processTask != null && this.processTask.IsCompleted))
            {
                this.processTask = this.ProcessGeneration();
            }
        }

        public bool SelectCell(int x, int y)
        {
            bool currentValue = this.world[x, y];
            return this.world[x, y] = !currentValue;
        }

        private Task ProcessGeneration()
        {
            return Task.Factory.StartNew(() =>
            {
                Parallel.For(0, Size, x =>
                {
                    Parallel.For(0, Size, y =>
                    {
                        int numberOfNeighbors = IsAlive(world, Size, x, y, -1, 0)
                            + IsAlive(world, Size, x, y, -1, 1)
                            + IsAlive(world, Size, x, y, 0, 1)
                            + IsAlive(world, Size, x, y, 1, 1)
                            + IsAlive(world, Size, x, y, 1, 0)
                            + IsAlive(world, Size, x, y, 1, -1)
                            + IsAlive(world, Size, x, y, 0, -1)
                            + IsAlive(world, Size, x, y, -1, -1);

                        bool shouldLive = false;
                        bool Alive = world[x, y];

                        if (Alive && (numberOfNeighbors == 2 || numberOfNeighbors == 3))
                        {
                            shouldLive = true;
                        }
                        else if (!Alive && numberOfNeighbors == 3) // zombification
                        {
                            shouldLive = true;
                        }

                        nextGeneration[x, y] = shouldLive;

                    });
                });
            });
        }

        private static int IsAlive(bool[,] world, int size, int x, int y, int offsetx, int offsety)
        {
            int results = 0;

            int proposedOffsetX = x + offsetx;
            int proposedOffsetY = y + offsety;
            bool outOfBounds = proposedOffsetX < 0 || proposedOffsetX >= size | proposedOffsetY < 0 || proposedOffsetY >= size;
            if (!outOfBounds)
            {
                results = world[x + offsetx, y + offsety] ? 1 : 0;
            }
            return results;
        }

        public void Update()
        {
            if (this.processTask != null && this.processTask.IsCompleted)
            {

                var flip = this.nextGeneration;
                this.nextGeneration = this.world;
                this.world = flip;
                Generation++;


                this.processTask = this.ProcessGeneration();

                if (NextGenerationCompleted != null) NextGenerationCompleted(this.world);
            }
        }
        public void Wait()
        {
            if (this.processTask != null)
            {
                this.processTask.Wait();
            }
        }
    }
}

