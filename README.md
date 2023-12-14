<p align="center"> 
    <img width="200px" height="300px" src="https://i.imgur.com/aLQ3826.png"/>
</p>

<h4 align="center" >🚀 News Scraping Monolithic 🚀</h4>

<h4 align="center">
  My solution for <a href="https://altamira.ifpa.edu.br/ target="_blank" >@IFPA</a> using C# and .NET Core 7 with Architecture to send via email news posted in intitute site
</h4>

<p align="center">
  |&nbsp;&nbsp;
  <a style="color: #8a4af3;" href="#project">Overview</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a style="color: #8a4af3;" href="#techs">Technologies</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
  <a style="color: #8a4af3;" href="#app">Project</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;
  <a style="color: #8a4af3;" href="#run-project">Run</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;
  <a style="color: #8a4af3;" href="#author">Author</a>&nbsp;&nbsp;&nbsp;|&nbsp;&nbsp;&nbsp;
</p>

#

<h1 align="center">
  
  <a target="_blank" href="https://github.com/wendellmoraisz">
    <img src="https://img.shields.io/static/v1?label=&message=wendellmoraisz&color=black&style=for-the-badge&logo=GITHUB"/>
  </a>

  <a target="_blank" href="https://www.linkedin.com/in/wendell-morais/">
    <img src='https://img.shields.io/static/v1?label=&message=Wendell%20Morais&color=black&style=for-the-badge&logo=LinkedIn'/> 
  </a>

</h1>

<p id="project"/>

<h2> | 💬 About:  </h2>

<p align="justfy">
  A problem at the institute I study is the dissemination of news. All news posted in institute site need a manual work to send the informations via WhatsApp. 
  Therefore, I decided to develop an application that automates this work and helps disseminate information within the institution.
</p>
<p>
  Furthermore, I have been studying software architecture, and I became interested in learning more about microservices. 
  So, I came up with the idea of developing an application using the traditional monolithic model and another one using microservices, both following the principles of Clean Architecture.
  With this, I intend to produce an article comparing the two solutions.
</p>

<br>

<h2 id="techs">
| 🧩 Technologies and Concepts Used:
</h2>
  
> <a target="_blank" href='https://learn.microsoft.com/pt-br/dotnet/csharp/'> <img width="60px" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/csharp/csharp-original.svg" /> </a>&nbsp;&nbsp;&nbsp;
<a target="_blank" href='https://dotnet.microsoft.com/'> <img width="60px" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/dotnetcore/dotnetcore-original.svg" /> </a>&nbsp;&nbsp;&nbsp;
 <a target="_blank" href='https://www.mysql.com/'> <img width="60px" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/mysql/mysql-original-wordmark.svg" /> </a>&nbsp;&nbsp;&nbsp;
 <a target="_blank" href='https://www.docker.com/'> <img width="60px" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/docker/docker-plain-wordmark.svg" /> </a>&nbsp;&nbsp;&nbsp;
<a target="_blank" href='https://git-scm.com/'> <img width="60px" src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/git/git-original-wordmark.svg" /> </a>&nbsp;&nbsp;&nbsp;

<br>

- Clean Architecture
- DDD
- Web Scraping
- MediatR Pattern
- FluentValitation
- Cron Jobs
- Quatz

> Among Others...
<br>

#

<h2 id="app">
  | 💻 Application:
</h2>

<br>

<p>
 The Application has an Cron Job that extract news from <a target="_blank" href="https://altamira.ifpa.edu.br/ultimas-noticias">IFPA Campus Altamira News Site</a> with Web Scraping and
  send the news via email for addresses registered in <a target="_blank" href="https://github.com/wendellmoraisz/emails-register-page">Emails Register Page</a>.
</p>

<p>
  This version is a traditional monolithic system based in Clean Architecture principles.
<p>

#

<h2 id="run-project"> 
   | 🛠️ How to use
</h2>

### Open your Git Terminal and clone this repository

```git
  $ git clone "git@github.com:wendellmoraisz/news-scraping-microservices.git"
```

### Make Pull

```git
  $ git pull "git@github.com:wendellmoraisz/news-scraping-microservices.git"
```

<br>

This application use `Docker` so you dont need to install and cofigurate anything other than docker on your machine.

<br>

Navigate to project folder and run it using `docker-compose`

```bash

  # After setup docker environment just run this commmand on root project folder:

  # For First Time run this command
  $ docker-compose -f docker-compose.yml -f docker-compose.override.yml up --build

  # To run project
  $ docker-compose -f docker-compose.yml -f docker-compose.override.yml up


```

<br>

#

<h2 id="author">
  | 🧑‍💻 Author:  
</h2>

> <a target="_blank" href="https://www.linkedin.com/in/wendell-morais/"> <img width="350px" src="https://instagram.fatm2-1.fna.fbcdn.net/v/t51.2885-15/364236795_658305826235139_7313126893555339390_n.heic?stp=dst-jpg_e35&efg=eyJ2ZW5jb2RlX3RhZyI6ImltYWdlX3VybGdlbi4xNDQweDE0NDAuc2RyIn0&_nc_ht=instagram.fatm2-1.fna.fbcdn.net&_nc_cat=103&_nc_ohc=jiK3Jgp18FIAX9aCnzv&edm=ACWDqb8BAAAA&ccb=7-5&ig_cache_key=MzE1Nzk0NTg3NDE1MTgwODk2Mw%3D%3D.2-ccb7-5&oh=00_AfDXD8WG7oupvawPVKJ_nnXk94Nf27W7vYU3osKg4K79MA&oe=65808644&_nc_sid=ee9879"/> <br> <p> <b> - Wendell Morais</b> </p></a>

<h1>
  <a target="_blank" href='https://github.com/wendellmoraisz'>
    <img src='https://img.shields.io/static/v1?label=&message=wendellmoraisz&color=black&style=for-the-badge&logo=GITHUB'> 
  </a>
  
   <a target="_blank" href='https://www.linkedin.com/in/wendell-morais/'>
    <img src='https://img.shields.io/static/v1?label=&message=Wendell%20Morais&color=black&style=for-the-badge&logo=LinkedIn'> 
  </a>
</h1>


<br>
