__SUB2.daystoexpire=1095; 

var __s2tQ =__s2tQ || []; 


try{
	var r=window.location.host.toLowerCase();

	if ( r.indexOf('uk.rs-online.com')>=0 ) {
		SUB2.licensekey = '7cbbf8ed-7c6e-49af-9164-495afa9f0e90';
		__SUB2.clientid = '7cbbf8ed-7c6e-49af-9164-495afa9f0e90';
	} else if ( r.indexOf('us.rs-online.com')>=0 ) {
		SUB2.licensekey = '73cffe8a-3bed-43e6-91dc-cacddad3fc48';
		__SUB2.clientid = '73cffe8a-3bed-43e6-91dc-cacddad3fc48';
    	} else if ( r.indexOf('fr.rs-online.com')>=0 ) {
		SUB2.licensekey = '59afc532-b60e-4053-9b78-ae4ee177cdd8';
		__SUB2.clientid = '59afc532-b60e-4053-9b78-ae4ee177cdd8';
    	} else if ( r.indexOf('it.rs-online.com')>=0 ) {
		SUB2.licensekey = '6439983c-79dd-4739-afd6-26b60cc4cb3c';
		__SUB2.clientid = '6439983c-79dd-4739-afd6-26b60cc4cb3c';
    	} else if ( r.indexOf('de.rs-online.com')>=0 ) {
        SUB2.licensekey = '8ed7718b-ce3c-465c-b670-131cdde499ed';
        __SUB2.clientid = '8ed7718b-ce3c-465c-b670-131cdde499ed';
    	} else if ( r.indexOf('au.rs-online.com')>=0 ) {
        SUB2.licensekey = 'd75797bc-4e6a-485f-ab86-cb0b712d21cb';
        __SUB2.clientid = 'd75797bc-4e6a-485f-ab86-cb0b712d21cb';
    	} else if ( r.indexOf('twen.rs-online.com')>=0 ) {
        SUB2.licensekey = 'd430df6c-4a56-44ff-b77a-c99d69841fdd';
        __SUB2.clientid = 'd430df6c-4a56-44ff-b77a-c99d69841fdd';
    	} else if ( r.indexOf('jp.rs-online.com')>=0 ) {
		SUB2.licensekey = 'bb9bff6d-71e3-4c62-8a0e-31be73dec446';
		__SUB2.clientid = 'bb9bff6d-71e3-4c62-8a0e-31be73dec446';
    	} else if ( r.indexOf('nl.rs-online.com')>=0 ) {
        	SUB2.licensekey = '4e8701b8-ccee-455a-a69b-f8f2a423cdb2';
        	__SUB2.clientid = '4e8701b8-ccee-455a-a69b-f8f2a423cdb2';
    	} else if ( r.indexOf('ie.rs-online.com')>=0 ) {
        	SUB2.licensekey = '12235b18-6ce4-402f-b130-c33be5d3e8b1';
        	__SUB2.clientid = '12235b18-6ce4-402f-b130-c33be5d3e8b1';
    } else if ( r.indexOf('es.rs-online.com')>=0 ) {
        SUB2.licensekey = '1e3cd1f9-5a88-44b1-b0fc-e8797cb787f7';
        __SUB2.clientid = '1e3cd1f9-5a88-44b1-b0fc-e8797cb787f7';
    } else if ( r.indexOf('at.rs-online.com')>=0 ) {
        SUB2.licensekey = '6185f6e0-7fa5-4f69-bd9e-434a10776f8a';
        __SUB2.clientid = '6185f6e0-7fa5-4f69-bd9e-434a10776f8a';
    } else {
	SUB2.licensekey = 'c9657125-3049-44a7-86d4-4e2cb4d0dad0';
	__SUB2.clientid = 'c9657125-3049-44a7-86d4-4e2cb4d0dad0';
	}	

}catch(e){ SUB2.clog('ERROR:CustomScript ' + e.message); }


try {
  
  if (document.readyState === "loading") {
    document.addEventListener("DOMContentLoaded", firePixel);
  } else {
    firePixel();
  }

  function firePixel() {
    if (window.location.href.includes("https://uk.rs-online.com/web/mb/arduino/")) {
		
		
		const downloadButton = document.querySelector(
		  'a[href="https://share-eu1.hsforms.com/1jGGgfCTgQguvWR-4fCQjLwfrdff"]'
		);

		if (downloadButton) {
		  downloadButton.addEventListener("click", () => {
			SUB2_Digital.createGtagdata(
			  "DC-14196390|event|conversion|{'allow_custom_scripts': true,'send_to': 'DC-14196390/rs-co000/rs-wh0+standard'}"
			);
		  });
		}
		
		const browseArduinoRange = document.querySelector(
			'a[href="https://uk.rs-online.com/web/c/raspberry-pi-arduino-development-tools/arduino-shop/?intcmp=UK-WEB-_-BP-HB-_-0624_NE-_-arduino_hardware_from_okdo"]'
		);
		
		if (browseArduinoRange) {
		  browseArduinoRange.addEventListener("click", () => {
			SUB2_Digital.createGtagdata(
			  "DC-14196390|event|conversion|{'allow_custom_scripts': true,'send_to': 'DC-14196390/rs-co001/rs-ar0+standard'}"
			);
		  });
		}
		
		const buyNow1 = Array.from(document.querySelectorAll('a')).find(a =>
		  a.href.includes("/web/p/stem-robot-kits/2842240") &&
		  a.querySelector('span')?.textContent.trim() === "Buy Now"
		);
		
		if (buyNow1) {
		  buyNow1.addEventListener("click", () => {
			SUB2_Digital.createGtagdata(
			  "DC-14196390|event|conversion|{'allow_custom_scripts': true,'send_to': 'DC-14196390/rs-co001/rs-ar0+standard'}"
			);
		  });
		}

		const buyNow2 = Array.from(document.querySelectorAll('a')).find(a =>
		  a.href.includes("/web/p/arduino/2662937") &&
		  a.querySelector('span')?.textContent.trim() === "Buy Now"
		);
		
		if (buyNow2) {
		  buyNow2.addEventListener("click", () => {
			SUB2_Digital.createGtagdata(
			  "DC-14196390|event|conversion|{'allow_custom_scripts': true,'send_to': 'DC-14196390/rs-co001/rs-ar0+standard'}"
			);
		  });
		}
	
		const buyNow3 = Array.from(document.querySelectorAll('a')).find(a =>
		  a.href.includes("/p/plcs-programmable-logic-controllers/2600885") &&
		  a.querySelector('span')?.textContent.trim() === "Buy Now"
		);
		
		if (buyNow3) {
		  buyNow3.addEventListener("click", () => {
			SUB2_Digital.createGtagdata(
			  "DC-14196390|event|conversion|{'allow_custom_scripts': true,'send_to': 'DC-14196390/rs-co001/rs-ar0+standard'}"
			);
		  });
		}
	}
  }
} catch (e) {
  SUB2.clog("CustomPixelFire: " + e.message);
}