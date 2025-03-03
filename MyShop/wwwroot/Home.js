
const visibleRegister = () => {
    const registerDiv = document.querySelector(".unvisible")
    registerDiv.classList.remove("unvisible")
}
const visibleUpdate = () => {
    const id = sessionStorage.getItem('user')
    if (id == null) {
        alert("to update your details first log in")
    }
    else { 
    window.location.href="UserDetails.html"}
}

const getDetailes = () => {
    const user = {
        Email: document.querySelector(".UserName").value,
        Password: document.querySelector(".Password").value,
        FirstName: document.querySelector(".FirstName").value,
        LastName: document.querySelector(".LastName").value
       
    }
    return user
}
const UpDateDetailes = () => {
    const user = {
        Email: document.querySelector(".UserNameUpDate").value,
        Password: document.querySelector(".PasswordUpDate").value,
        FirstName: document.querySelector(".FirstNameUpDate").value,
        LastName: document.querySelector(".LastNameUpDate").value
    }
    return user
}
const login = async () => {
    const login = {
        Email: document.querySelector(".UserNameLogin").value,
        Password: document.querySelector(".PasswordLogin").value

    }
    try {
        const response = await fetch(`api/users/login/?Email=${login.Email}&Password=${login.Password}`, {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },

            query: { Email: login.Email, Password :login.Password}
        });
        const postData = await response.json();
        console.log('postData:', postData.userId)
        sessionStorage.setItem('user', postData.userId)
        if (JSON.parse(sessionStorage.getItem('Cart'))==[])
            window.location.href = "Products.html"
        else
            window.location.href = "ShoppingBag.html"

    }
    catch (error) {
        alert("not valed")
    }

}



const Register = async() => {
    const user = getDetailes()
    const progress = document.querySelector(".Progress");
    if (progress.value <= 2) {
        alert("password to short")
    }
    else { 
    try {
        const response = await fetch("https://localhost:7141/api/users", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: await JSON.stringify(user)
        });
        alert(response)
    }
    catch (error) {
        alert("not valed")
    }
    }
}
const Update = async () => {
    const id = sessionStorage.getItem('user')
        const Detailes = UpDateDetailes();
        try {
            const response = await fetch(`https://localhost:7141/api/users/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json'
                },
                body: await JSON.stringify(Detailes)
            });
            alert("Updated details")
            window.location.href="Products.html"
        }
        catch (error) {
            alert("not valed")
        }
    

}
    const checkPassword = async() => {
        const password = document.querySelector(".Password").value;
        const progress = document.querySelector(".Progress");
       
        try {
            const response = await fetch("api/users/password", {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json'
                },
                body:JSON.stringify(password)
            });
            const data = await response.json();
            console.log(data);
            progress.value = data;

        }
        catch (error) {
            alert("password not valid")
        }


}

 
        
    

    



