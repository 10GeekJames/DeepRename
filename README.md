<h1> Safely and recursively rename folders, files, and file content</h1>
<sub>A super quick, super tragic, might make it cooler one day, but gets the job done way to rename everything everywhere</sub>

<h3>Getting Started</h3>

- Zip up what you wish to rename in a common .zip format
- Place that next to this .exe with the name deeprename.zip
- Run this .exe

Answer the 2 questions when prompted
```
    What word should I search for?
    What word should I replace it with?
```

The system will unpack the zip, work through file-contents, file-names, and folder names attempting to rename 3 variations

Example
``` 
 MyFirstProjectILove
 MyTenthProjectBaseFromFirst
```

Becomes
```
 MyFirstProjectILove
 myFirstProjectILove
 MYFIRSTPROJECTILOVE
 & 
 MyTenthProjectBaseFromFirst
 myTenthProjectBaseFromFirst
 MYTENTHPROJECTBASEFROMFIRST
```

attempts reworks are case-senstive, but, expanded to the pattern respectively

