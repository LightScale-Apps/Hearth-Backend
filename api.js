class APIConnection {
  constructor() {
    this.URL = "http://ec2-18-189-105-20.us-east-2.compute.amazonaws.com/api/";
    this.token = "";
    this.email = "";
    this.password = "";
    this.data = {};
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

  async getAccessToken(code) {
    let response = await fetch(this.URL + `auth/${this.email}/${code}`, {
      method: "GET",
      headers: {
        Accept: "*/*",
      },
    });

    let data = await response.json();
    this.token = data.token;
  }

  async getPatientData(property) {
    let path = this.URL + "patient";
    if (property !== undefined) {
      path += `/${property}`;
    }
    let response = await fetch(path, {
      method: "GET",
      headers: {
        Accept: "*/*",
        Authorization: `Bearer ${this.token}`,
      },
    });

    let dataObject = await response.json();
    if (property === undefined) {
      this.data = dataObject;
    } else {
      this.data[property] = dataObject[property];
    }
    return dataObject;
  }

  async postPatientData(dataObject) {
    let response = await fetch(this.URL + "patient", {
      method: "POST",
      headers: {
        Accept: "*/*",
        "Content-Type": "application/json-patch+json",
        Authorization: `Bearer ${this.token}`,
      },
      body: JSON.stringify(dataObject),
    });
  }
}

//Usage:

user = new APIConnection(); //create new connection object

user.email = "ryanbhillis@gmail.com";
user.password = "mypasswordsuXXX:3";

user.getOneTimeCode(); //takes their username and password and sends to server

//the line below will get the token from the server using the OTC
//user.getAccessToken("6752");

// user.getPatientData();

// //the data is stored in the user object
// console.log(user.data);

// console.log(user.token); //here is the token
