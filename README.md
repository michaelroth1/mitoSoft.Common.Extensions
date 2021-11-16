# mitoSoft.Common.Extensions

This library provides search function for pattern search between curly brackets {...}.
In case of the input string 'test message form {find}' and the following code snipped

```c#
s = s.ReplaceBetweenBrackets("find", "mitoSoft");
```

the output is generated as 'test message form {mitoSoft}'.

By using this function it s also possible to replace the date in the actual string like

'some message form {find} at {date}'

to 

'some message form mitoSoft at Dec 24'.

The following code snipped represents these relationships

```c#
    var s = "some message form {find} at {date}.";
    
    s = s.ReplaceBetweenBrackets("find", "mitoSoft");
    s = s.ReplaceBetweenBrackets("date", new DateTime(2021, 12, 24).ToString("MMM dd", System.Globalization.CultureInfo.InvariantCulture));
    
    //output: "some message form mitoSoft at Dec 24.";
```

Furthermore it is also possible to format the date like

```c#
    var s = "{{Date:yyyy}-{DATE:MM}-{Date:dd} {date:HH:mm}}";

    s = s.ReplaceFormattedDate(new DateTime(1982, 3, 7, 6, 0, 0, DateTimeKind.Utc));
	
	//output: "{1982-03-07 06:00}";
```

to get '{1982-03-07 06:00}' as a result.

For more examples see the testclasses in [testproject](mitoSoft.Common.Extensions.Tests).