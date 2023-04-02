import crypto from "k6/crypto";
import encoding from "k6/encoding";

const algToHash = {
    HS256: "sha256",
    HS384: "sha384",
    HS512: "sha512"
};

function sign(data, hashAlg, secret) {
    let hasher = crypto.createHMAC(hashAlg, secret);
    hasher.update(data);

    // Some manual base64 rawurl encoding as `Hasher.digest(encodingType)`
    // doesn't support that encoding type yet.
    return hasher.digest("base64").replace(/\//g, "_").replace(/\+/g, "-").replace(/=/g, "");
}

function encode(payload, secret, algorithm) {
    algorithm = algorithm || "HS256";
    let header = encoding.b64encode(JSON.stringify({ typ: "JWT", alg: algorithm }), "rawurl");
    payload = encoding.b64encode(JSON.stringify(payload), "rawurl");
    let sig = sign(header + "." + payload, algToHash[algorithm], secret);
    return [header, payload, sig].join(".");
}

function decode(token, secret, algorithm) {
    let parts = token.split('.');
    let header = JSON.parse(encoding.b64decode(parts[0], "rawurl"));
    let payload = JSON.parse(encoding.b64decode(parts[1], "rawurl"));
    algorithm = algorithm || algToHash[header.alg];
    if (sign(parts[0] + "." + parts[1], algorithm, secret) != parts[2]) {
        throw Error("JWT signature verification failed");
    }
    return payload;
}

const generateJwt = (userId) => {
    let payload = { sub: userId };
    let token = encode(payload, "secret");
    return "Bearer " + token;
}

export default generateJwt;