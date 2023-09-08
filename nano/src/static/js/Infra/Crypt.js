
export function Crypt(message) {
  let key = CryptoJS.enc.Utf8.parse(process.env.CRYPT_KEY);
  let iv = CryptoJS.enc.Utf8.parse(process.env.CRYPT_KEY);
  let encrypted = CryptoJS.AES.encrypt(CryptoJS.enc.Utf8.parse(message), key,
    {
      keySize: 128 / 8,
      iv: iv,
      mode: CryptoJS.mode.CBC,
      padding: CryptoJS.pad.Pkcs7
    });
  return encrypted.toString();
}
