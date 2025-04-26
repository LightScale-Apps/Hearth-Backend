class APIConnection {
  constructor(email, password) {
    this.URL = "http://ec2-18-189-105-20.us-east-2.compute.amazonaws.com/api/";
    this.token = "";
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

  async login(email, password) {
    const res = await fetch(this.URL + "auth/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
        accept: "*/*",
      },
      body: JSON.stringify({ email, password }),
    });

    await console.log(res);

    this.token = await res.json().token;
  }
}
