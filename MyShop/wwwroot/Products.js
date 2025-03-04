const getFilters = () => {
    document.getElementById("PoductList").innerHTML=''
    const filter = {
        desc: document.querySelector("#nameSearch").value,
        minPrice: document.querySelector("#minPrice").value,
        maxPrice: document.querySelector("#maxPrice").value,
        categoryIds: JSON.parse(sessionStorage.getItem("categoryIds"))||[],
        position:0,
        skip: 0
         //JSON.parse(sessionStorage.getItem("Category"))
    }
    return filter
}

const showOneProduct = async (product) => {
    let tmp = document.getElementById("temp-card");
    let cloneProduct = tmp.content.cloneNode(true)
    cloneProduct.querySelector("img").src = "./images/" + product.image
    cloneProduct.querySelector("h1").textContent = product.name
    cloneProduct.querySelector(".price").innerText = product.price
    cloneProduct.querySelector(".description").innerText = product.description
    cloneProduct.querySelector("button").addEventListener('click', () => { addToCart(product) })
    document.getElementById("PoductList").appendChild(cloneProduct)
}
const addToCart = async(product) => {
    //if (sessionStorage.getItem('user')) {
    //let count = document.getElementById("ItemsCountText").innerHTM
    let a = JSON.parse(sessionStorage.getItem("amount"))
    sessionStorage.setItem("amount", JSON.stringify(a+1))
    let cart = JSON.parse(sessionStorage.getItem('Cart'))
    let count = cart.length
    let userId = JSON.parse(sessionStorage.getItem("user"))
    /* if (userId != undefined) {*/
    let cartId = -1
    for (let i = 0; i < count; i++) {
        if (cart[i].productId == product.id) {
            cartId=i
        }
    }
    if (cartId != -1) {
        cart[cartId].quantity = cart[cartId].quantity + 1
    }
     else {
        let newItem = { productId: product.id, quantity: 1 }
        await cart.push(newItem)
    }
    document.getElementById("ItemsCountText").innerHTML = a+1
      sessionStorage.setItem('Cart', JSON.stringify(cart))
    //}
   
    //else {
    //    alert("you are not a valid user")
    //    window.location.href ="home.html"
    //}
       


    
}

const showAllProducts=(products) => {
    for (let i = 0; i < products.length; i++) {
        showOneProduct(products[i]);
    }
}
const GetProductList =  async () => {
    const filterItems = getFilters();
    //alert(filterItems)
    let url = `api/product/?position=${filterItems.position}&skip=${filterItems.skip}`
    if (filterItems.desc != '')
        url += `&desc=${filterItems.desc}`
    if (filterItems.minPrice != '')
        url += `&minPrice=${filterItems.minPrice}`
    if (filterItems.maxPrice != '')
        url += `&maxPrice=${filterItems.maxPrice}`
    if (filterItems.categoryIds != '')
        for (let i = 0; i < filterItems.categoryIds.length; i++) { 
            url += `&categoryIds=${filterItems.categoryIds[i]}`}
    try {
        const Productss = await fetch(url, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json'
            },

            query: {
                position: filters.position, skip: filters.skip, desc: filters.desc, minPrice: filters.minPrice,
                maxPrice: filters.maxPrice, categoryIds:filters.categoryIds
            }
            /*body: await JSON.stringify(filters)*/
           
        });
        const postData = await Productss.json();
        console.log('postData:', postData)
        showAllProducts(postData)
    }
    catch (error) {
        alert("not valid")
    }
}
const productList = addEventListener("load", async () => {
    sessionStorage.setItem("amount", sessionStorage.getItem("amount")||0)
    //sessionStorage.setItem("categories" , [])
    GetProductList()
   
    
})
const GetCategory = addEventListener("load", async () => {
    GetCategories()
    //sessionStorage.setItem("categories", JSON.stringify([]))
    //sessionStorage.setItem('Cart', sessionStorage.getItem('Cart') || JSON.stringify([]))
    //let myCartArr = JSON.parse(sessionStorage.getItem("Cart")) || [];
    ////let count = JSON.parse(sessionStorage.getItem('Cart')).length + 1
    //document.querySelector("#ItemsCountText").innerHTML = myCartArr.length
    ////document.getElementById("ItemsCountText").innerHTML = count

    let categoryIdArr = [];
    let myCartArr = JSON.parse(sessionStorage.getItem("Cart")) || [];
    sessionStorage.setItem("categoryIds", JSON.stringify(categoryIdArr))
    sessionStorage.setItem("Cart", JSON.stringify(myCartArr))
    //sessionStorage.setItem("count")
    //document.querySelector("#ItemsCountText").innerHTML = myCartArr.length
    document.querySelector("#ItemsCountText").innerHTML = JSON.parse(sessionStorage.getItem("amount"))
    
    

})
const filterProducts = async () => {
    GetProductList()
}
const GetCategories = async () => {
    const categoryList = await fetch('api/Category', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        },


    })
    const Categories = await categoryList.json();
    console.log('Category:', Categories)
    showCategories(Categories)
}
const showCategories = async (Categories) => {
    for (let i = 0; i < Categories.length; i++) {
        let tmp = document.getElementById("temp-category");
        let cloneCategory = tmp.content.cloneNode(true)
        cloneCategory.querySelector(".opt").addEventListener("change", () => {selectedCategories(Categories[i].id)})
        cloneCategory.querySelector(".OptionName").innerText = Categories[i].name
        document.getElementById("categoryList").appendChild(cloneCategory)
    }
}
const selectedCategories =async(id) => {
    let categories = JSON.parse(sessionStorage.getItem("categoryIds"))
    let a = categories.indexOf(id)
    a == -1 ? categories.push(id) : categories.splice(a, 1)
    await sessionStorage.setItem("categoryIds", JSON.stringify(categories))
    console.log(categories)
    GetProductList()
    

  
}
//const clearAll = async () => {
//    //document.querySelector("#nameSearch").innerText = ""
//    //document.querySelector("#minPrice").innerText = ""
//    //document.querySelector("#maxPrice").innerText = ""
//    //sessionStorage.setItem("categoryIds", JSON.stringify([]))
//    GetProductList()
//    //window.location.href="Products.html"
//}


