### TOP 100 Celebrities

This is a small weekend project I made for an job interview in a company

The system presents a table of Celebrities.  
For each celebrity it shows name, birth date, gender, occupation and a picture.  
The user has a limited capability to edit the cotent.

### Technical Specification:  
The components are built on top of:
- Client: ASP.NET Web Forms
- Server: .Net Web API
- DB: [JSON Flat File Data Store](https://github.com/ttu/json-flatfile-datastore)
- Scraper: [OpenScraping](https://github.com/microsoft/openscraping-lib-csharp)

### Implementation:  
- The content is scraped from IMDB's [100 MOST POPULAR CELEBRITIES IN THE WORLD](https://www.imdb.com/list/ls052283250) page. 
- The scraping implementation is a part of project
- This data is stored in a JSON file.  
- The user may delete individual celebrities from the table, which will also delete it from JSON.  
- The user may also reset the whole table to original form. The data will be re-scraped from IMDB.
- The operations of reset and deletion are run at server side and are requested by client through Web API  

### Usage:
Just run it with Visual Studio
