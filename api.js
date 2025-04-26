class APIConnection {
  constructor(email, password) {
    this.URL = "http://ec2-18-189-105-20.us-east-2.compute.amazonaws.com/api/";
    this.token = "";
    this.email = email;
    this.password = password;
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

  async login() {
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

    await console.log(res);

    this.token = await res.json().token;
    return this.token;
  }
}

api = new APIConnection("ryanbhillis@gmail.com", "mypasswordsuXXX:3");
