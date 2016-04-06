using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.Worlds {
    public class Map {

        Vector2[] _movements = new Vector2[]
	    {
		new Vector2(-1, -1),
		new Vector2(0, -1),
		new Vector2(1, -1),
		new Vector2(1, 0),
		new Vector2(1, 1),
		new Vector2(0, 1),
		new Vector2(-1, 1),
		new Vector2(-1, 0)
	    };
        public int[][] Tiles { get; private set; }
        public int[][] Distance { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public float Size { get; private set; }

        public bool CanMoveTo(Vector2 position) {
            var scaled = position / Size;
            if (scaled.X > Width || scaled.X < 0) {
                return false;
            } else if (scaled.Y > Height || scaled.Y < 0) {
                return false;
            } else if ((Tiles[(int)scaled.X][(int)scaled.Y] & 1) == 0) {
                return false;
            } else {
                return true;
            }
        }
        private IEnumerable<Vector2> ValidMoves(int x, int y) {
            // Return each valid square we can move to.
            foreach (Vector2 movePoint in _movements) {
                var newVec = new Vector2(x + movePoint.X, y + movePoint.Y);
                if (CanMoveTo(newVec)) {
                    yield return newVec;
                }
            }
        }
        public void Pathfind(Vector2 startingPoint) {
            startingPoint /= this.Size;
            // Find path from hero to monster. First, get coordinates of hero.
            int heroX = (int)startingPoint.X;
            int heroY = (int)startingPoint.Y;
            if (heroX == -1 || heroY == -1) {
                return;
            }
            // Hero starts at distance of 0.
            Distance[heroX][heroY] = 0;

            while (true) {
                bool madeProgress = false;

                // Look at each square on the board.
                for (int x = 0; x < Width; x++) {
                    for (int y = 0; y < Height; y++) {
                        // If the square is open, look through valid moves given
                        // the coordinates of that square.
                        if ((Tiles[x][y] & 1) == 1) {
                            int passHere = Distance[x][y];

                            foreach (Vector2 movePoint in ValidMoves(x, y)) {
                                int newX = (int)movePoint.X;
                                int newY = (int)movePoint.Y;
                                int newPass = passHere + 1;

                                if (Distance[newX][newY] > newPass) {
                                    Distance[newX][newY] = newPass;
                                    madeProgress = true;
                                }
                            }
                        }
                    }
                }
             

                    
                if (!madeProgress) {
                    break;
                }
            }
        }

        public int GetTile(Vector2 position) {
            return this.Tiles[(int)Math.Floor(position.X)][(int)Math.Floor(position.Y)];
        }

        public bool CanMoveTo(Vector2 currentPosition, Vector2 nextPosition) {
            // Do not count pixels |dx|==|dy| diagonals twice:
            var currentScaled = currentPosition / Size;
            var nextScaled = nextPosition / Size;
            var delta = nextScaled - currentScaled;
            int steps = (int)(Math.Abs(delta.X) == Math.Abs(delta.Y)
                    ? Math.Abs(delta.X) : Math.Abs(delta.X) + Math.Abs(delta.Y));

            var step = delta / steps;

            for(int k = 0; k < steps; k++)
            {
                currentScaled += step;
                if ((this.Tiles[(int)Math.Floor(currentScaled.X)][(int)Math.Floor(currentScaled.Y)] & 1) == 0) {
                    return false;
                }
            }
            return true;
        }


        public void SetTiles(int top, int left, int width, int height, int value) {
            for (int i = top; i < top + height; i++) {
                for (int j = left; j < left + width; j++) {
                    this.Tiles[i][j] = value;
                }
            }
        }

        public Map(int height, int width, float size) {

            this.Width = width;
            this.Height = height;
            this.Size = size;
            this.Tiles = new int[width][];
            for (int i = 0; i < width; i++) {
                this.Tiles[i] = new int[height];
            }
            this.Distance = new int[width][];
            for (int i = 0; i < width; i++) {
                this.Distance[i] = new int[height];
            }
            
        }

        public void Draw() {
        }

 
    }
}
