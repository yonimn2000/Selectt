# *Selectt*

*Oakland University - CSI 3370 - Software Engineering - Winter 2021*

A simple anonymous polling web application.

[Engineering and Demo Video of *Selectt*](https://youtu.be/BclOXeF8T98)

## Problem Statement

Polling anonymity is extremely important, meaning that a vote of an individual should not be tied to that individual. So, a fully functional web application that will allow anonymous voting and polling will be developed. Users will be able to register, create polls, and invite others to vote via an email link. The system will handle the integrity of one vote per person to ensure an accurate vote count, user privacy, and anonymity. Votes will be aggregated in the database so that individual votes will not be stored in the system.

## Objectives

* Allow registered users to create anonymous polls by inviting people to vote by email.
* Allow invited voters to vote without registering into the system.
* Store the votes in such a way that they cannot be tied to individual votes.
* Allow voters to see live vote count as soon as they vote.

## Team Members

* Jacob Malek ([jmalek41](https://github.com/jmalek41))
* Yonatan Mankovich ([yonimn2000](https://github.com/yonimn2000))
* Adam Marszalek ([gpadam-code](https://github.com/gpadam-code))
* Kyle McBride ([kamcbride](https://github.com/kamcbride))
* Jasmin Medic ([Jazz-jpg](https://github.com/Jazz-jpg))
* Andrew Miller ([AndrewMiller510](https://github.com/AndrewMiller510))

## Who We Are

We are a group of students at Oakland University majoring in Computer Science, Information Technology, and Computer Engineering. This website is for a Software Engineering course project that we are all collaborating on. We wanted to make a website that would have a practical applications in the real world, so we came up with *Selectt* as a voting platform.ting on.ting on.ting on.

## What *Selectt* Is

*Selectt* is a unique voting application that ensures a single vote is carried out per an invited individual respondent.

## How We Did It

We accomplished voter integrity and anonymity by using tokenized email links in order to allow poll creators to send out invites to respondents who wish to vote on a poll. The website then ensures that if you receive an email, you are guaranteed a single vote. No cheesing the system here! All votes are then displayed on a nice chart that the poll creator and the voters can view.

## Why We Did It

This project was a class requirement of ours, so we decided to have some fun with it. In light of the recent political climate we felt there was a need to create something that could unite us in the most old fashioned form of democracy, Voting!

## What Technologies We Used

* C#
* ASP.NET MVC Framework
* Entity Framework
* Razor Pages
* HTML
* CSS
* Bootstrap
* JavaScript
* Chart.js
* jQuery
* Google Fonts
* Font-Awesome Icons
* Coffee
* Energy Drinks

## Frequently Asked Questions

### How Do I Make a Poll?

After registering and logging in, simply head over to the home page via the selector at the top left part of the screen. That will take you to your polls page where you will be able to see a list of all your polls. In the header you will find a button labeled "Create a New Poll". Click it and fill in your poll information, then press "Create". Congratulations! You just created your first poll! Additionally press the edit button and begin to add answers and respondents to your poll. Once you are satisfied with your poll, save your changes and check back in on the polls page where you can then send in your poll. Be warned though, once you send your poll you can no longer update the information of the poll.

### Who Can View My Poll?
When you create or edit a poll, you have the option to toggle whether or not the poll results are public. This means that when your send your poll out, your respondents will or will not be able to see the results.

### What Can I Do After I Make a Poll?
After making your poll you can send the poll out to respondents you have named in your mailing list. Subsequently, your respondents will vote on your poll. To view the results of the poll simply hang out in the results page denoted by a pie chart button on your home page. This will graphically display your results in real time!

## References

* Bootstrap was used as the CSS framework: [GetBootstrap.com](https://getbootstrap.com)
* jQuery was used as a JavaScript library: [jQuery.com](https://jquery.com)
* Toggle switch was drafted from documentation gathered from [HtmlLion.com](https://www.htmllion.com/css3-toggle-switch-button.html)
* Chart.js was the javascript framework used to develop pie and bar charts: [ChartJs.org](https://chartjs.org)
* Google Fonts was used to generate the fonts for our project: [fonts.google.com](https://fonts.google.com)
* Font-Awesome Icons was used for button Icons: [FontAwesome.com](https://fontawesome.com/icons)
* Open source image website from which the ballot-box icon was used: [CleanPng.com](https://www.cleanpng.com/png-ballot-box-blob-emoji-election-7086945/download-png.html)

## Screenshots

### Home Page

![image](media/home-page.png)

### Poll Creation Page

![image](media/poll-create.png)

### Polls List

![image](media/poll-list.png)

### Poll Edit Page

![image](media/poll-edit.png)
![image](media/poll-edit2.png)

### The Email

![image](media/vote-email.png)

### Voting Page

![image](media/vote-page.png)

### Results Page

![image](media/results-table.png)
![image](media/results-pie.png)
![image](media/results-bar.png)

### Pollster Poll Details View After Getting Some Poll Votes

![image](media/poll-details.png)
