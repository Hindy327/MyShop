const getCartById = async(item) => {
    try {
        const response = await fetch(`https://localhost:7141/api/product/${item.productId}`, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },
        });
        const Data = await response.json();
        console.log('postData:', Data)
        showProduct(Data, item.quantity)
         //return Data
    }
    catch (error) {
        alert("not valed")
    } 
    
}

const getCart = async () => {
 

   let cartArray = JSON.parse(sessionStorage.getItem("Cart"))
     cartArray.map((item) => {
         //let product =
              getCartById(item)
       // console.log(product)

    })
}




const CartList = addEventListener("load", async () => {
    document.querySelector("#totalAmount").innerHTML = 0
    document.querySelector("#itemCount").innerHTML = 0
/*    sessionStorage.setItem('currentItem')*/
    getCart()


})
const showProduct = async (product,quantity) => {
    let tmp = document.getElementById("temp-row");
    let cloneProduct = tmp.content.cloneNode(true)
    let url = `./images/${product.image}`

    cloneProduct.querySelector(".image").style.backgroundImage = `url(${url})`
    cloneProduct.querySelector(".itemName").innerText = product.description
    cloneProduct.querySelector(".price").innerText = product.price 
   //cloneProduct.querySelector("#itemCount").innerText = product.price

    //cloneProduct.querySelector(".availabilityColumn").innerText = product.description
    let sum = JSON.parse(document.querySelector("#totalAmount").innerHTML) + product.price*quantity
    document.querySelector("#totalAmount").innerHTML = sum
    let count = JSON.parse(document.querySelector("#itemCount").innerHTML) + quantity
    document.querySelector("#itemCount").innerHTML = count
    cloneProduct.querySelector(".itemsCount").innerText = quantity
    cloneProduct.querySelector(".expandoHeight").addEventListener('click', () => { deleteProduct(product) })
    document.getElementById("items").appendChild(cloneProduct)
}
const deleteProduct = (product) => {
    let cartArray = JSON.parse(sessionStorage.getItem('Cart'))
    cartArray.map((item) => {
        if (item.productId == product.id) {
            item.Quantity = item.Quantity - 1
        }

    })
    sessionStorage.setItem('Cart', JSON.stringify(cartArray))
    document.getElementById("itemsInCart").innerHTML = ''
    getCart()
    let amount = JSON.parse(sessionStorage.getItem("amount"))
    sessionStorage.setItem("amount", JSON.stringify(amount - 1))


}

    //let cartArray = JSON.parse(sessionStorage.getItem("Cart"))
    //let id = cartArray.indexOf(product.id)
    //cartArray.splice(id, 1)
    //sessionStorage.setItem("Cart", JSON.stringify(cartArray))
    //document.getElementById("items").innerHTML = ''
    //window.location.href ="ShoppingBag.html"
    //getCart()

    const placeOrder = async () => {
        let orderItems = JSON.parse(sessionStorage.getItem('Cart'))
        orderPostCart(orderItems)


}
const orderPostCart = async (orderItems) => {
    const orderPost = orderPostObj(orderItems)
    try {
        const response = await fetch("https://localhost:7141/api/order", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(orderPost)
        })
        const Data = await response;
        console.log('postData:', Data)
    }
    catch (error) {
        alert("not valid")
    }
}
const orderPostObj = (orderItems) => {
    const order = {
        UserId: JSON.parse(sessionStorage.getItem('user')),
        OrderDate:"15-01-2025",
        OrderSum: JSON.parse( document.querySelector("#totalAmount").innerHTML),
        OrderItems: orderItems
    }
    return order

}
const generateDate = () => {

    Datedate = new DateTime(2005, 12, 12, 9, 0, 0);
    Console.log(date);
    //Console.WriteLine("Year = " + date.Year.ToString());
    //Console.WriteLine("Month = " + date.Month.ToString());
    //Console.WriteLine("Day = " + date.Day.ToString());
    //Console.WriteLine("Hour = " + date.Hour.ToString());
    //Console.WriteLine("Minute = " + date.Minute.ToString());
    //Console.WriteLine("Second = " + date.Second.ToString());
    //const date = new Date();
    //let day = date.getDay();
    //let month = date.getMonth() + 1;
    //let year = date.getFullYear();
    let currentDate = `${date.Year}-${date.Month}-${date.Day}`;
    let hh = `${date.Year}-${date.Month}-${date.Day}`;
    //let dt = new Date(year, month, day)
    return currentDate
}

     


    
       