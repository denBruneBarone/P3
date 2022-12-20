import random
import string
import time
from selenium.webdriver.common.by import By

def redirect(driver, url, delay=.5):
    driver.get(url)
    time.sleep(delay)

def click_on_a_tag(driver, name, delay=.5):
    redirect(driver, driver.find_element(by=By.NAME, value=name).get_attribute("href"), delay)

def write_to_textbox(driver, name, value):
    driver.find_element(by=By.NAME, value=name).send_keys(value)
    time.sleep(.3)
    
def generate_random_string(prefix=None, _len=5):
    rnd =  ''.join(random.choices(string.ascii_lowercase, k=_len)) 
    return rnd if prefix is None else prefix+"_" +rnd

def get_item_if_exists(driver, name, by=By.NAME):
    try: return True, driver.find_element(by=by, value=name)
    except Exception: return False, None