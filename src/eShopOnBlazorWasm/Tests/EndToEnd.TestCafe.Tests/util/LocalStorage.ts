import { ClientFunction } from "testcafe";
export const getLocalStorage = ClientFunction((key: string) => {
  return new Promise<string>(resolve => {
    const result = localStorage.getItem(key);
    resolve(result);
  });
});

export const setLocalStorage = ClientFunction((key: string, value: string) => {
  return new Promise<void>((resolve, reject) => {
    try {
      localStorage.setItem(key, value);
      resolve();
    } catch (error) {
      reject(error);
    }
  });
});
