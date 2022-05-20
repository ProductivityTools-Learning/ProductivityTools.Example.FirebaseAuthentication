from flask import Flask
from flask_cors import CORS
from flask_restful import Api
import os

from Resources.Date import DateResource

def create_app():
    app=Flask(__name__)
    CORS(app)
    register_resources(app)

def register_resources(app):
    api=Api(app)
    api.add_resource(DateResource,"/Date")

if __name__=="__main__":
    app=create_app()
    app.run(port=int(os.environ.get("PORT", 8080)),host='0.0.0.0',debug=True)

