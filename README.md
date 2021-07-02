
<h1> Safely and recursively rename folders, files, and file content</h1>
<sub>A super quick, gets the job done way to rename everything everywhere and sort of know what changes took place. Works against .zip files to keep your original files safe.</sub>

- Quick Video Tour (3-min) https://www.youtube.com/watch?v=BxEC-0aQ650

- Lengthier Tour (9-min) https://www.youtube.com/watch?v=ZH4EnRQ5nVI  

<h3>Getting Started</h3>

- Zip up what you wish to rename in a common .zip format
- Place that next to this .exe with the name deeprename.zip
- Run this .exe
- Final result will be deeprename_renamed.zip with all changes included

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

If you have <a href="https://winmerge.org/downloads/?lang=en" target="_blank"> Winmerge </a> installed at default locations a side by side comparisson of the deeprename_filecontentsonly.zip and the deeprename.zip will be pulled up for you. 

deeprename_filecontentsonly.zip has file-level renames applied how ever, file names and folder names remain unchanged that common comparison tools might be used to see these changes at a granular level.

```
Input:
 deeprename.zip

Output:
 deeprename_renamed.zip
 deeprename_filecontentsonly.zip
 deeprename_report.xlsx
```

