using Raylib_CsLo;
using static Raylib_CsLo.Raylib;

const int MAX_BUILDINGS = 100;
const int ScreenWidth = 800;
const int ScreenHeight = 450;

InitWindow(ScreenWidth, ScreenHeight, "raylib [core] example - 2d camera");

var player = new Rectangle(400, 280, 40, 40);
var buildings = new Rectangle[MAX_BUILDINGS];
var buildColors = new Color[MAX_BUILDINGS];

var spacing = 0;
for (var i = 0; i < MAX_BUILDINGS; ++i)
{
    buildings[i] = new()
    {
        width = GetRandomValue(50, 200),
        height = GetRandomValue(100, 800),
        y = ScreenHeight - 130.0f - buildings[i].height,
        x = -6000.0f + spacing,
    };

    spacing += (int)buildings[i].width;

    buildColors[i] = new(
        GetRandomValue(200, 240),
        GetRandomValue(200, 240),
        GetRandomValue(200, 250),
        255
    );
}

var camera = new Camera2D
{
    target = new(player.x + 20.0f, player.y + 20.0f),
    offset = new(ScreenWidth / 2.0f, ScreenHeight / 2.0f),
    rotation = 0.0f,
    zoom = 1.0f,
};

while (!WindowShouldClose())
{
    if (IsKeyDown(KeyboardKey.KEY_RIGHT))
        player.x += 2;
    else if (IsKeyDown(KeyboardKey.KEY_LEFT))
        player.x -= 2;

    camera.target = new(player.x + 20, player.y + 20);

    if (IsKeyDown(KeyboardKey.KEY_A))
        camera.rotation--;
    else if (IsKeyDown(KeyboardKey.KEY_S))
        camera.rotation++;

    if (camera.rotation > 40)
        camera.rotation = 40;
    else if (camera.rotation < -40)
        camera.rotation = -40;

    camera.zoom += ((float)GetMouseWheelMove() * 0.05f);

    if (camera.zoom > 3.0f)
        camera.zoom = 3.0f;
    else if (camera.zoom < 0.1f)
        camera.zoom = 0.1f;

    if (IsKeyPressed(KeyboardKey.KEY_R))
    {
        camera.zoom = 1.0f;
        camera.rotation = 0.0f;
    }

    BeginDrawing();

    ClearBackground(RAYWHITE);

    BeginMode2D(camera);

    DrawRectangle(-6000, 320, 13000, 8000, DARKGRAY);

    for (int i = 0; i < MAX_BUILDINGS; i++)
        DrawRectangleRec(buildings[i], buildColors[i]);

    DrawRectangleRec(player, RED);

    DrawLine(
        (int)camera.target.X,
        -ScreenHeight * 10,
        (int)camera.target.X,
        ScreenHeight * 10,
        GREEN
    );
    DrawLine(
        -ScreenWidth * 10,
        (int)camera.target.Y,
        ScreenWidth * 10,
        (int)camera.target.Y,
        GREEN
    );

    EndMode2D();

    DrawText("SCREEN AREA", 640, 10, 20, RED);

    DrawRectangle(0, 0, ScreenWidth, 5, RED);
    DrawRectangle(0, 5, 5, ScreenHeight - 10, RED);
    DrawRectangle(ScreenWidth - 5, 5, 5, ScreenHeight - 10, RED);
    DrawRectangle(0, ScreenHeight - 5, ScreenWidth, 5, RED);

    DrawRectangle(10, 10, 250, 113, Fade(SKYBLUE, 0.5f));
    DrawRectangleLines(10, 10, 250, 113, BLUE);

    DrawText("Free 2d camera controls:", 20, 20, 10, BLACK);
    DrawText("- Right/Left to move Offset", 40, 40, 10, DARKGRAY);
    DrawText("- Mouse Wheel to Zoom in-out", 40, 60, 10, DARKGRAY);
    DrawText("- A / S to Rotate", 40, 80, 10, DARKGRAY);
    DrawText("- R to reset Zoom and Rotation", 40, 100, 10, DARKGRAY);

    EndDrawing();
}

CloseWindow();
