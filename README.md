# StatisticSearch

Getting Started

##### The code: 
Clone the code files from 
             
1. git clone https://github.com/ivanpchelnikov/StatisticSearch.git
2. Build API in the Visiual Studio or from CLI inside solution folder: StatisticSearch:
      <b>run dotnet</b>
3. If client fails to auto build, please, build client:"
	   cd StatisticSearch\StaticSearch\ClientApp
	   npm install

##### Web page:
```
https://staticsearch20210205100818.azurewebsites.net/
```
##### Web Api:
```
https://staticsearch20210205100818.azurewebsites.net/update/{keywords}/{urltag}
```
I have use simple serach quiries for each search engine:

for google: 
```
/search?q={keywords}&num=100
```
for bing: 
```
/search?q={keywords}&count=100
```
It is easy to extend to more search providers
##### Technical details:

- Dependency injection to separate service implementation from servcie interface.
- Inheritance: all services uses absract class where I pass the folloing parameters:
- Using MemoryCache for keeping results for 60 minutes unchanged, keep the history of latest search untill restart of application. Settings parameters are configured in appsetting.json file. 
- For scraping html page use .Net core library  HtmlAgilityPack.
  We have to prepare html tags, which repeats on html page and wrapp the search result. 
  Sample is 
 ```
   <div class= "tF2Cxc" > 
     searchresult ...
   </div>
 ```  
converts to 
```
"//div[contains(@class, 'tF2Cxc')]"
```
For UI I use react SPA with javascripts to update the result.
