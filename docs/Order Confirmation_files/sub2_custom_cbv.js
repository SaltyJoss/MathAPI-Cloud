SUB2.codebaseversion='0.0';

try {
    var r = window.location.host.toLowerCase();
    
    /*
    var excludedDomains = [
        'uk.rs-online.com',
        'us.rs-online.com',
        'fr.rs-online.com',
        'it.rs-online.com',
        'de.rs-online.com',
        'au.rs-online.com',
        'twen.rs-online.com',
        'jp.rs-online.com',
        'nl.rs-online.com',
        'ie.rs-online.com',
        'es.rs-online.com',
        'at.rs-online.com'
    ];

    if (!excludedDomains.some(domain => r.includes(domain))) {
        SUB2.codebaseversion='0.0';
    }
    */

    var includedDomains = [
        'uk.rs-online.com',
        'de.rs-online.com'
    ];

    if (includedDomains.some(domain => r.includes(domain))) {
        SUB2.codebaseversion='0.0'; 
    }

} catch (e) {
    SUB2.clog('ERROR:CustomScript ' + e.message);
}