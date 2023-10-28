const si = require("systeminformation");

class HardwareHelper {

	/**
	 * @type {si.Systeminformation.StaticData & si.Systeminformation.DynamicData}
	 */
	systemInformation;

	constructor()
	{
		this.init();
	}

	async init() {
		this.systemInformation = await si.getAllData();
	} 
}

module.exports = HardwareHelper;