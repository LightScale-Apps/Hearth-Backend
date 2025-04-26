class APIConnection {
  constructor() {
    this.URL = "http://ec2-18-189-105-20.us-east-2.compute.amazonaws.com/api/";
    this.token = "";
    this.email = "";
    this.password = "";
  }

  async request(url) {
    const res = await fetch(this.URL + url, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        accept: "*/*",
      },
      body: "",
    });
    return await res;
  }

  async getOneTimeCode() {
    const res = await fetch(this.URL + "auth/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        Accept: "*/*",
      },
      body: `{
        "email": "${this.email}",
        "password": "${this.password}"
      }`,
    });
  }

  async getAccessToken(oneTimeCode) {
    let response = await fetch(this.URL + `auth/${this.email}/${oneTimeCode}`, {
      method: "GET",
      headers: {
        Accept: "*/*",
      },
    });

    let data = await response.json();
    this.token = data.token;
  }
}

//Usage:

user = new APIConnection(); //create new connection object

user.email = "ryanbhillis@gmail.com";
user.password = "mypasswordsuXXX:3";

user.getOneTimeCode(); //takes their username and password and sends to server

//the line below will get the token from the server using the OTC
//user.getAccessToken("187613.18273681.1823763.1872638");

console.log(user.token); //here is the token
