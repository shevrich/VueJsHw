new Vue({
    el: '#div',

    mounted: function () {
        this.loadPeople();
    },

    data: {
        newPerson: {},
        people: [],
        cars: [],
        carshasvalue: false,
        newCar: {},
        personId: ''
    },

    methods: {
        showModal: function () {
            this.person = '';
            $(".person-modal").modal();
        },
        savePerson: function () {
            $.post('/home/addperson', this.newPerson, () => {
                $(".modal").modal('hide');
                this.loadPeople();
                this.newPerson = {};
            });
            
        },
        loadPeople: function () {
            $.get('/home/getpeople', people => {
                this.people = people;
            });
        },
        viewCars: function (person) {
            $.get('/home/getCars', { personId: person.id }, cars => {
                this.cars = cars;
                if (this.cars.length > 0) {
                    this.carshasvalue = true;
                }
            });
            this.carshasvalue = !this.carshasvalue;
        },
        addCar: function(person){
            this.personId = person.id;
            $(".car-modal").modal();
        },
        saveCar: function () {
            $.post('/home/addCar', { car: this.newCar, personId: this.personId }, () => {
                $(".modal").modal('hide');
                this.newCar = {};
            });
        },
    }
});