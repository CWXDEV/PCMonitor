const HardwareHelper = require("../helpers/HardwareHelper")

const HH = new HardwareHelper();

setInterval(() => {

	console.log(HH.systemInformation.currentLoad.currentLoad);
}, 5000)

