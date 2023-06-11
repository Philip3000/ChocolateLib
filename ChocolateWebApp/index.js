Vue.createApp({
    data() {
        return {
            items: [],
            addItemData: {productNo: 0, chocolateType: "", price: 0, inStock: 0},
            updateItem: {productNo: 0, chocolateType: "", price: 0, inStock: 0},
            ProductNo: 0,
            InStock: 0,
            stockAmount: 0,
            idItem: {},
            filteredItems: [],
            numberTo: 0,
            ChocolateType: "",
            errorMessage: "",
            Price: 0,
            baseUrl: "https://charlottesstockapi20230611194946.azurewebsites.net/api/EasterEggs/",
        }
    },
    async created() {
    await this.getAllItems();
  },
  methods: {
    async getAllItems() {
      try {
        const response = await axios.get(this.baseUrl);
        this.items = await response.data;
      } catch (ex) {
        alert(ex.message);
      }
    },
    async filterByStock() {
        try {
            const response = await axios.get(this.baseUrl + "lowStock/" + this.stockAmount)
            this.filteredItems = await response.data;
            if (this.filteredItems.length == 0) {
                this.errorMessage = "No items found with stock less than:" + this.stockAmount
            }
        }
        catch(ex) {
            alert(ex.message)
        }
    },
    async getById() {
        try {
            const response = await axios.get(this.baseUrl);
            this.items = await response.data;
            for (let i = 0; i < this.items.length; i++) {
                if (this.numberTo === this.items[i].productNo) {
                    this.idItem = this.items[i]
                }
            }
        }
        catch (ex) {
            alert(ex.message)
        }
    },
    async createNew() {
        try {
            response = await axios.post(this.baseUrl, this.addItemData)
            this.getAllItems()
        }
        catch (ex) {
            alert(ex.message)
        }
    },
    sortBrand() {
        this.items.sort((item1, item2) => item1.chocolateType.localeCompare(item2.brand))
    },
    sortQualityAsc() {
        this.items.sort((item1, item2) => item1.inStock - item2.inStock)
    },
    async update() {
        try {
        console.log(this.updateItem)
        response = await axios.put(this.baseUrl, this.updateItem)
        console.log(response)
        this.getAllItems()
        }
        catch (ex) {
            alert(ex.message)
        }
    }
  },
}).mount("#app")
