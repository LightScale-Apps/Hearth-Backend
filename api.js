class APIConnection {
  constructor() {
    this.URL = "http://ec2-18-189-105-20.us-east-2.compute.amazonaws.com/api/";
    this.token = "";
    this.email = "";
    this.password = "";
    this.data = {};
    this.chatConnection = new signalR.HubConnectionBuilder()
      .withUrl("/chat", { accessTokenFactory: () => this.token })
      .build();
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

  async getModelResponse(input) {
    if (this.chatConnection.state !== "Connected") {
      await this.chatConnection.start();
    }
    return this.chatConnection.invoke("SendMessage", input);
  }
}

//Usage:

user = new APIConnection(); //create new connection object

user.email = "ryanbhillis@gmail.com";
user.password = "NewPassword808!";

user.getOneTimeCode(); //takes their username and password and sends to server

//the line below will get the token from the server using the OTC
//user.getAccessToken("6752");
//the token will be stored inside of the user object

// user.getPatientData();

// //the data is stored in the user object
// console.log(user.data);

//user.getPatientData("property") //gets a specific property

//to post patient data with a given json object
//user.postPatientData({
//    property: value,
//    anotherProperty, anotherValue
//});

// console.log(user.token); //here is the token
//
