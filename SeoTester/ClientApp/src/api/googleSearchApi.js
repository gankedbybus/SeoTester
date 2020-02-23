export const get = async endpoint => {
  return await fetch(endpoint, {
    headers: {
      Accept: 'application/json',
      'Content-Type': 'application/json'
    }
  });
};

export const getGoogleSearchRanks = async (keyWords, url) => {
  return await get(`googlesearch/getranks/?keyWords=${keyWords}&url=${url}`);
};
