## **Unity Developer Technical Interview Task Submission**

##### new Update on 2024-11-06 #####
- Hosting the static webgl build at [AWS s3](http://truescape-webgl.s3-website-ap-southeast-2.amazonaws.com/)
- Add the procedural skybox shader graph with clouds. 
- Setup the sky with three exposed color for sky horizon and ground
- Add clouds layer with animation and some controls. 
 
##### new Update on 2024-11-05 #####
 
 I have Implemented a **vegetation management system** that loads/unloads environmental assets based on camera position and mining stage. 
 - Load tree in to object pool. 
 - Make different configs (density, radius, culling distance, scale range) for different stagings. 
 - Dynamic changes the rendering base on stage and camera culling result. 
 

### ** Instruction**
  The task is hosted at [AWS s3](http://truescape-webgl.s3-website-ap-southeast-2.amazonaws.com/)
   - Click the flashing play button at the center of screen to load the 1st scene. 
   - 1st scene will play a video and show a screenshot of mining site on top right. 
   - User can click the Point of interest Pin on the site to enter the second scene(Stage Visualization). 
   - User could press the button at bottom left to switch between stages. 
   - User can use left click+drag for camera rotation, right click+drag for panning around amd Wheeling for zooming.
   - The basic stats like FPS and GPU memory usage display on top left. 
   - TrueScape logo at bottom right and NorthPoint at top right. 

---

### **What I have Developed**

   **Set Up Addressable Asset System**
   - Use **Addressable package** to manage assets like models, textures, and UI elements.

   **Profiling and Optimization**
   - implement a simple stats checker to display framerate and GPU load.
   - Optimize texture compression settings (e.g., ASTC for WebGL), mesh quality, and asset bundle sizes.
   - Write a bat script to compress textures and meshes. （ 392MB -> 44MB)
   - script included in the zip file. 

   **Scene Management**
   - Implement basic scene loading and unloading using `SceneManager.LoadSceneAsync` and `SceneManager.UnloadSceneAsync` when switching from first scene to staging scene. 
   - Create a **Play button** for handling scene loading feedback. 

   **Stage Visualization**

   - **Stage Transitions**:
     - Implement a simple dynamic button for smooth transition between stages and ensure they trigger relevant loading/unloading actions.
     - Use compressed mesh and texture assets for optimized loading on WebGL.
     - Implement **object pooling** for frequently instantiated items, like terrains.


   **Camera and Controls**
   - Set up an **orbital camera system** (similar to Google Earth) for intuitive exploration.
   - Define controls for:
   - **Left Click + Drag** for rotation.
   - **Right Click + Drag** for panning.
   - **Scroll Wheel** for zoom.
   - Adjust camera speed and smoothness to match the scale of the scene
   - use **ScriptableObjects** for camera configuration

   **Interactive Elements and UI**
   - Place an **interactive marker** on the 1st scene, representing a point of interest.
   - Use UI panel to display a **WebGL-compatible video**.

   **Testing**
      - Perform comprehensive testing, including scene transitions, asset loading/unloading, and marker interactions.
---

### What can be improved ###

As I am currently employed full-time at my company, I began working on this task on November 3, 2024. Due to time constraints, there are several aspects I am confident I can achieve, but I have not yet completed them.

   - Create shaders to simulate **lighting**, **shadows**, or atmospheric effects if appropriate.
   - Implement a **vegetation and environment management system** that loads/unloads environmental assets based on camera position.
   - Consider custom **LODs** or **GPU instancing** for high vegetation density scenes.
   - Use Unity Shader Graph to dynamically generate **water**, **sky**, **wind** and more.  
---




