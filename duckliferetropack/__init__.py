import typing
import os, json
from .Options import DuckLifeRetroPack_options
from .Items import item_table, DuckLifeRetroPackItem
from .Locations import location_table, DuckLifeRetroPackLocation
from BaseClasses import Item, ItemClassification, Tutorial
from worlds.AutoWorld import World


class DuckLifeRetroPack(World):
    """This is the description of My Game that will be displayed on the AP
       website."""
    
    game: str = "Duck Life: Retro Pack"

    item_id_to_name = item_table
    location_id_to_name = location_table
    option_definitions = DuckLifeRetroPack_options

    def create_item(self, name: str) -> Item:
        return DuckLifeRetroPackItem(name, ItemClassification.progression, item_table[name], self.player)
