using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using Project3.Screens;
using System.Collections.Generic;

namespace Project3
{
    public enum GameState
    {
        Start,
        Playing,
        Paused,
        End,
    }
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        //screens
        Start startScreen;
        End endScreen;
        Pause pauseScreen;
        //variables
        internal static Dictionary<string, Texture2D> textureDictionary = new Dictionary<string, Texture2D>();
        public static GameState state = GameState.Start;
        //we need a start screen
        //also need a rendertarget
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.IsFullScreen = true;
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 480;
            graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
           //graphics.GraphicsDevice.Viewport.Bounds used for bounds
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            startScreen = new Start(Content);
            // TODO: use this.Content to load your game content here

            //textureDictionary.Add("Player", Content.Load<Texture2D>("ship")); Load textures to key
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            //default code
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                Exit();
            //check for input, handle only the first touch given
            TouchLocation touch = TouchPanel.GetState()[0]; 

            // TODO: Add your update logic here
            switch (state)
            {
                case GameState.Start:
                    startScreen.Update(touch);
                    break;
                case GameState.Playing:
                    break;
                case GameState.End:
                    endScreen.Update(touch);
                    break;
                case GameState.Paused:
                    pauseScreen.Update(touch);
                    break;
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin(); //do something with this to rescale
            // TODO: Add your drawing code here
            /*enemies.ForEach((Enemy enemy)=>{
                enemy.Draw(ref spriteBatch);
            });*/
            switch(state)
            {
                case GameState.Start:
                    startScreen.Draw(spriteBatch);
                    break;
                case GameState.Playing:
                    break;
                case GameState.End:
                    endScreen.Draw(spriteBatch);
                    break;
                case GameState.Paused:
                    pauseScreen.Draw(spriteBatch);
                    break;
            }

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
