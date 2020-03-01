namespace Tetris.Data
{
    public class Tetrominoes
    {
        public const string TetrominSymbol = "■";
        //T_Block
        public static int[][,] T_Block = {
            new int[,]
            {
                {0,0 },{1,0 },{0,1 },{-1,0 }
            },
            new int[,]
            {
                { 0,0},{ 0,1},{ -1,0},{ 0,-1}
            },
            new int[,]
            {
                { 0, 0},{ 1, 0},{ -1, 0},{0, -1}
            },
            new int[,]
            {
                {0,0 },{1,0 },{0,1 },{0,-1 }
            }
        };

        //S_Block
        public static int[][,] S_Block = {
            new int[,]
            {
                { 0, 0},{-1,0},{0,-1},{1,-1}
            },
            new int[,]
            {
                {0,0},{1,0},{1,1},{0,-1}
            },
        };

        //Z_Block
        public static int[][,] Z_Block = {
            new int[,]
            {
                { 0, 0},{1,0},{-1,-1},{0,-1}
            },
            new int[,]
            {
                {0,0},{-1,1},{-1,0},{0,-1}
            },
        };

        //I_Block
        public static int[][,] I_Block = {
            new int[,]
            {
                {0,0},{-1,0},{-2,0},{1,0}
            },
            new int[,]
            {
                { 0, 0},{0,1},{0,-1},{0,-2}
            }
        };

        //L_Block
        public static int[][,] L_Block = {
            new int[,]
            {
                {0,0},{1,0},{-1,1},{-1,0}
            },
            new int[,]
            {
                {0,0},{0,1},{-1,-1},{0,-1}
            },
            new int[,]
            {
                {0,0},{1,0},{-1,0},{1,-1}
            },
            new int[,]
            {
                { 0, 0},{0,1},{1,1},{0,-1}
            },
        };

        //J_Blok
        public static int[][,] J_Block = {
            new int[,]
            {
                {0,0},{1,0},{1,1},{-1,0}
            },
            new int[,]
            {
                { 0, 0},{0,1},{-1,1},{0,-1}
            },
            new int[,]
            {
                {0,0},{-1,0},{-1,-1},{1,0}
            },
            new int[,]
            {
                {0,0},{0,1},{0,-1},{1,-1}
            }
        };

        //O_Block
        public static int[][,] O_Block = {
            new int[,]
            {
                { 0, 0},{0,-1},{1,-1},{1,0}
            }
        };
    }
}
