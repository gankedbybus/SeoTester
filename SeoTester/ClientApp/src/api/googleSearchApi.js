export const getGoogleSearchRanks = async (keyWords, url) => {
  const response = await fetch(
    `googlesearch/getranks/?keyWords=${keyWords}&url=${url}`,
    {
      headers: {
        Accept: 'application/json',
        'Content-Type': 'application/json'
      }
    }
  );

  return response;
};
