import React, { Component } from 'react';

export class FetchData extends Component {
    static displayName = FetchData.name;

    constructor(props) {
        super(props);
        this.state = { searchList: [], keyword: "", searchTag: "", loading: true };
    }

    componentDidMount() {
        this.populateWeatherData();
    }

    makeSearch = () => {
        if (this.state.keyword === "" || this.state.searchTag === "") {
            alert("You must enter a keyword and search tag.");
        }
        else {
            this.populateWeatherData();
        }
    }

    myChangeSearchHandler = (event) => {
        let nam = event.target.name;
        let val = event.target.value;
        this.setState({ [nam]: val });
    }

    static renderSearchTable(searchList) {
        return (
            <table id='mainT' className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Google</th>
                        <th>Bing</th>
                    </tr>
                </thead>
                <tbody>
                    {searchList.map(search =>
                        <tr key={search.date}>
                            <td>{search.date}</td>
                            <td>{search.google}</td>
                            <td>{search.bing}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }

    render() {
        let contents = this.state.loading
            ? <p><em>Loading...</em></p>
            : FetchData.renderSearchTable(this.state.searchList);

        const mystyle = {
            paddingLeft: "10px",
            height: "30px",
            float: "right"
        }

        return (
            <div>
                <div>
                    <button style={mystyle} type="button" onClick={this.makeSearch}>Search</button>
                    <h1 id="tabelLabel" >Statistic list.</h1>
                </div>

                <div>
                    <input style={mystyle} name="searchTag" id="searchTag" type="text" onChange={this.myChangeSearchHandler} />
                    <label style={mystyle}>Search Tag:</label>
                    <input style={mystyle} name="keyword" id="keyword" type="text" onChange={this.myChangeSearchHandler} />
                    <label style={mystyle}>Keyword:</label>
                    <label>This list demonstrates data from the 2 searh engine: Google and Bing.</label>
                </div>
                {contents}
            </div>
        );
    }

    async populateWeatherData() {
        let response = [];
        if (this.state.keyword === "" || this.state.searchTag === "") {
            response = await fetch('statisticsearch',
                {
                    method: 'GET',
                    headers: {
                        'Access-Control-Allow-Headers': '*',
                        'Content-Type': 'application/json',
                        'Access-Control-Allow-Origin': '*'
                    }
                });
        }
        else {
            response = await fetch('statisticsearch/update/' + this.state.keyword + '/' + this.state.searchTag,
                {
                    method: 'GET',
                    headers: {
                        'Access-Control-Allow-Headers': '*',
                        'Content-Type': 'application/json',
                        'Access-Control-Allow-Origin': '*'
                    }
                });
        }
        const data = await response.json();
        this.setState({ searchList: data, loading: false });
    }
}
