## README.md for MAO, a Minimalistic Access to OpenAI 

### Overview
This project intends to provide a minimalistic gateway for developers interested in a Web solution to access OpenAI. The example provided is in VB.NET. If you want another language, simply run the translation through any of the AI agents available; I was able to run it in C# and PHP with no problems. I unapologetically like VB.NET the most. 

### Prerequisites
- A modern web browser that supports HTML5 and JavaScript ES6.
- An OpenAI API key (for OpenAI mode). Load the key in an environmental variable called OPENAI_API_KEY. 
- ASP.NET-capable Web server (e.g. IIS, Apache+mono, etc)

### Installation
Simply clone or download the repository to your local machine. Map the folder to your Web server.

### File Structure
- `openai.html`: The main HTML file that includes web component tags and links to JavaScript files.
- `OpenAIHandler.ashx` : Server-side component communicating via AJAX with openai.html. 
- `jquery.min.js` : jQuery v3.5.1
- `showdown.min.js` : showdown v2.1.0

The last two files could be linked (the URL is commented out in source code); however, experience has shown me that it is better to secure all the source you need for your projects. As far as I can tell, there is no license violation. 


### Debugging
Open the Developer tools in your browser (usually F12). Check the console. All important messages appear there. 

### License
This project is open-sourced and available under CC-BY-SA.

### Contact
For any queries or suggestions, please contact biomathematicus (Google it) or raise an issue against the repository.

