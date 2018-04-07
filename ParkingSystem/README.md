# Smart automated parking payment system

Business application which detects license plates with image processing, basing on photos taken by a camera installed under the entrance barrier.
Application communicates with opencv program sending photo to C++ project.
It returns license plate. 
Information is saved in the database and there are windows alerts about events assigned to the section.

Application send communication about car's arrive.
Arrival time is saved in database with read license number.
In logs there is an information about parking beginning - it is flagged with a green arrow.
In the case of sending the same photo to the database, driver gets an e-mail about costs from wp.pl domain.
Meanwhile in logs there is an information about end of parking - it is flagged with a red arrow.
Parking time is calculated from car's arrive to departure. Cost is 3z³ per 1 hour.

Technologies:
1. WPF - graphical subsystem which renders application window.
2. Server which listens on port 12358 for all post requests with photos in .jpg format.
3. Application communicates with opencv project using proxy.
4. SQLite database using Entity Framework 
