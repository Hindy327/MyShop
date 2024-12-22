const getFilters = () => {
    const filter = {
        desc: document.querySelector("#nameSearch").value,
        minPrice: document.querySelector("#minPrice").value,
        maxPrice: document.querySelector("#maxPrice").value,
        categoryIds: [1,2],
        position:0,
        skip:0
    }
    return filter
}
const addToCart = async () => {

}
const showOneProduct = async (product) => {
    let temp = document.getElementById("temp-card");
    let cloneProduct = temp.content.cloneNode(true)
    cloneProduct.querySelector("img").src = "./images/" + product.image
    cloneProduct.querySelector("h1").textContent = product.name
    cloneProduct.querySelector(".price").innerText = product.price
    cloneProduct.querySelector(".description").innerText = product.description
    cloneProduct.querySelector(".button").addEventListener('click', () => {
        addToCart(product)
    })
    document.getElementById(productList).appendChild(cloneProduct)
}
const showAllProducts=(products) => {
    for (let i = 0; i < products.length; i++) {
        showOneProduct(products[i]);
    }
}
const GetProductList =  async () => {
    const filters = getFilters();
    alert(filters)

    const url =`api/Product?position=${filters.position}&skip=${filters.skip}`
    if (desc!=null)
        url += `&desc=${filters.desc}`
    if (minPrice!=null)
        url += `&minPrice=${filters.minPrice}`
    if (maxPrice!=null)
        url += `&maxPrice=${filters.maxPrice}`
    /*url += `&categoryIds=${ filters.categoryIds[0] }`*/
    try {
        const Productss = await fetch(`api/Product/?position=${filters.position}&skip=${filters.skip}&desc=${filters.desc}&minPrice=${filters.minPrice}&maxPrice=${filters.maxPrice}&categoryIds=${filters.categoryIds}`, {
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
        alert("not valed")
    }
}
const productList = addEventListener("load", async() => {
    GetProductList()
})
const filterProducts = async () => {
    GetProductList()
}
