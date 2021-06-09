
## Starting Point :
[Setup Wizard](https://ravendb.net/docs/article-page/5.1/csharp/start/installation/setup-wizard) I run the install w Power Shell - first impression - the installer is a web app!
First hurdle : choose certification - as in like a cloud db or a local - why would I need cert for local?
Anyhow - ofc I wanted the far left  - security first!

<img src="https://ravendb.net/RavenFS/GetDocImage?v=4.2&lang=All&key=start/installation/setup-wizard&fileName=setup-wizard-1.png" width="300" >

But at this step I entered 127.0.0.1 with a warning that this This node won't be reachable from outside this machine. Found different information about it [(origins of dev site here)](https://issues.hibernatingrhinos.com/issue/RavenDB-12559) but I continued and then on the restart of the site step the error was that the 127.0.0.1 ip was already taken. Eventualy I did it again and again and it worked.
I still dont get what is the IP we need to enter - [this tutorial](https://www.techrepublic.com/article/how-to-install-the-ravendb-nosql-database-on-ubuntu-20-04/) & [youtube](https://www.youtube.com/watch?v=tMUIpi2VKJI) states  

"ServerUrl": "http://SERVER_IP:8080",  Where SERVER_IP is the IP address of your hosting server. I think it is a port forward which I have tried for torrents but did not succeed with it.


Going through the [founder's article](https://ayende.com/blog/180802/ravendb-setup-how-the-automatic-setup-works) on the new and improved automatic setup - it was updated June 2020 but its code is from 2012...
> we need to provide proof to Let’s Encrypt that we own the hostname in question. 
> During setup, the user will register a subdomain under that, such as arava.dbs.local.ravendb.net. We ensure that only a single user can claim each domain. Once they have done that, they let RavenDB what IP address they want to run on. This can be a public IP, exposed on the internet, a private one (such as 192.168.0.28) or even a loopback device (127.0.0.1).
> The local server, running on the user’s machine then initiates a challenge to Let’s Encrypt for the hostname in question. With the answer to the challenge, the local server then call to api.ravendb.net. This is our own service, running on the cloud. The purpose of this service is to validate that the user “owns” the domain in question and to update the DNS records to match the Let’s Encrypt challenge.

I went with the documentation example and this was on another page but found it : 
X509Certificate2 clientCertificate = new X509Certificate2("C:\\path_to_your_pfx_file\\cert.pfx");  
Finally We have CONTACT! :raised_hands:
Trying alot of data insert and delete using the beautiful web management console - next step: build a CRUD to see what works compared to a typical relational db
Fun facts from the documentation:
* Singleton Pattern for the documentstore - only one to exist to ensure that only  one SaveChanges does all the tracking with [Unit of Work Pattern](https://ravendb.net/docs/article-page/5.1/csharp/client-api/session/what-is-a-session-and-how-does-it-work#unit-of-work-pattern)
* Idientity Map Pattern - The first document Load() call goes to the server and fetches the document from the database.The document is then saved as an entity in the Session's [identity map.](https://martinfowler.com/eaaCatalog/identityMap.html)
* [IDocumentSession](https://ravendb.net/docs/article-page/5.1/csharp/client-api/session/opening-a-session#syntax) awesome detailed documentation
* [<Load>](https://martinfowler.com/eaaCatalog/identityMap.html) was confusing for Delete example in documentation but it was tried and has also the iQueriable for .Where(x=>x.id).FirstOrDefault(); 

## On the Console app development:
Great example RavenDemo and awesome plugin for [Faker](https://blog.elmah.io/easy-generation-of-fake-dummy-data-in-c-with-faker-net/) also has specific name and pronouns and email
Found great ways to beutify with [specter](https://spectreconsole.net/) Console :muscle: - was developed just [this year](https://www.hanselman.com/blog/spectreconsole-lets-you-make-beautiful-console-apps-with-net-core).
Tried to put emoticons in, no success but that would have been cool.

As I cannot find a right way to make Indexes work with any given string (see part below at your leisure) - I went for the extra credit :ok_woman: to use [Alice in Wonderland's text](https://gist.githubusercontent.com/phillipj/4944029/raw/75ba2243dd5ec2875f629bf5d79f6c1e4b5a8b46/alice_in_wonderland.txt) for the Pawn's bio generation
Parsing text [article of 2008](https://www.c-sharpcorner.com/uploadfile/scottlysle/parsing-sentences-and-building-text-statics-in-C-Sharp/) was just the thing :clap:

Video to do and fingers crossed!

***
### Notes on RavenDb search of a specific string in all values 
Looking into search method and [Query](https://ravendb.net/docs/article-page/5.1/csharp/client-api/session/querying/how-to-use-search) - what is boost - its a decimal for the [boost](https://ravendb.net/docs/article-page/4.2/csharp/indexes/querying/boosting) value -> Each search term can be associated with a boost factor that influences the final search results. The higher the boost factor, the more relevant the term will be.
Back on the search in Query for specific text -  found that options: SearchOptions.Or, escapeQueryOptions: EscapeQueryOptions.AllowAllWildcards was valid for up to Raven.Client 3.5 - now at 4.2 in a comment in the [Raven's official documentation](https://ravendb.net/docs/article-page/4.2/csharp/client-api/session/querying/how-to-use-search) page with "You can use `lucene()` method in the query for that"
Looks like they replaced all wildcards and escapequery for [Lucene](https://ravendb.net/articles/on-replacing-lucene) to rep
Apache Lucene is a free and open-source search engine software library, originally written completely in Java.
The core of every index is its mapping function that utilizes LINQ-like syntax, and the result of such a mapping is converted to a The core of every index is its mapping function that utilizes LINQ-like syntax, and the result of such a mapping is converted to a Lucene index entry which is persisted for future use to avoid re-indexing each time the query is issued and keep response times minimal.
[Working with Indexes](https://ravendb.net/learn/inside-ravendb-book/reader/4.0/12-working-with-indexes]   - there is a book <3 0 chapter 12 working with indexes
                                                                                                                                
                                                                                                                                
`pawns = session
                                                                                                                                
                   .Query<Pawn>().Where(x =>x.Email.Contains(inputText))
  
                  .Search(x => x.Email, inputText ) //Search does not work for *any* string - only for full email`
  
  
NotSupportedException: Contains is not supported, doing a substring match over a text field is a very slow operation, and is not allowed using the Linq API.
The recommended method is to use full text search (mark the field as Analyzed and use the Search() method to query it.
[Article Link](https://ravendb.net/docs/article-page/2.5/csharp/client-api/querying/static-indexes/configuring-index-options): You need to Define an Index and change the field as analyzed for ti to work > then Search will work
DatabaseCommands cannot be found as a class from store.DatabaseCommands  > [article](https://ravendb.net/docs/article-page/4.2/csharp/migration/client-api/commands) it has been removed in 4.2 > found article in [stackoverflow](https://stackoverflow.com/questions/19656130/linq-value-contains-function-error) where the documentation is un-understandable , refers to >Operations : [How to Start an Index](https://ravendb.net/docs/article-page/4.2/csharp/client-api/operations/maintenance/indexes/start-index)
store.Maintenance.Send(new StartIndexOperation("Orders/Totals"));  

`pawns = session.Query<Pawns_Search.Result, Pawns_Search>() //<-Result cannot be found in that scope
                                                                     
                    //     .Where(p => p.Query == inputText)
  
                    //     .OfType<Pawn>()
  
                    //     .ToList();`

  
Found a solution but usin s.Adbanced.LuceneQuery<Item, WhateverYouCalledThatIndex>()  
Found how [to use In](https://stackoverflow.com/questions/7899936/ravendb-how-to-query-with-multiple-search-terms#answer-7902851)

`using Raven.Client.Linq;
Resource[] FindResourcesByEmployees(string[] employeeIds)
{
    return this.Session.Query<Resource>()
        .Where(r => r.EmployeeId.In<string>(employeeIds)))
        .ToArray();
}`

 Discovered that Result was a created class within the Search - but 
> errorRaven.Client.Exceptions.RavenException: 'System.ArgumentException: The field 'Query' is not indexed, cannot query/sort on fields that are not indexed
   at Raven.Server.Documents.Indexes.Index.ThrowInvalidField(String f) in C:\Builds\RavenDB-Stable-5.1\51027\src\Raven.Server
Th search works for full email search - more research needed for any string search as RavenDb is tricky on that one :dancers:
